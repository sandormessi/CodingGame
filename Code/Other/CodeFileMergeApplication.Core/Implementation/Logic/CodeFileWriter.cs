namespace CodeFileMergeApplication.Core.Implementation.Logic
{
   using System;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;

   using CodeFileMergeApplication.Core.Abstraction.Data;
   using CodeFileMergeApplication.Core.Abstraction.Logic;

   using CoreUtilities.Abstraction.FileSystem;

   public class CodeFileWriter : ICodeFileWriter
   {
      #region Constants and Fields

      private const int InitialStringBuilderCapacity = 1024 * 4;

      private readonly IFileWriter fileWriter;

      #endregion

      #region Constructors and Destructors

      public CodeFileWriter(IFileWriter fileWriter)
      {
         this.fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
      }

      #endregion

      #region ICodeFileWriter Members

      public void WriteCodeFile(CodeFile codeFile, string targetPath)
      {
         if (codeFile == null)
         {
            throw new ArgumentNullException(nameof(codeFile));
         }

         if (string.IsNullOrWhiteSpace(targetPath))
         {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(targetPath));
         }

         string codeFileText = CreateTextToWrite(codeFile);
         fileWriter.WriteAllText(codeFileText, targetPath, Encoding.UTF8);
      }

      public Task WriteCodeFileAsync(CodeFile codeFile, string targetPath)
      {
         return WriteCodeFileAsync(codeFile, targetPath, CancellationToken.None);
      }

      public async Task WriteCodeFileAsync(CodeFile codeFile, string targetPath, CancellationToken cancellationToken)
      {
         if (codeFile == null)
         {
            throw new ArgumentNullException(nameof(codeFile));
         }

         if (string.IsNullOrWhiteSpace(targetPath))
         {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(targetPath));
         }

         string codeFileText = await Task.Factory.StartNew(() => CreateTextToWrite(codeFile), cancellationToken).ConfigureAwait(false);
         await fileWriter.WriteAllTextAsync(codeFileText, targetPath, Encoding.UTF8, cancellationToken).ConfigureAwait(false);
      }

      #endregion

      #region Methods

      private static string CreateTextToWrite(CodeFile codeFile)
      {
         StringBuilder codeFileTextStringBuilder = new(InitialStringBuilderCapacity);

         foreach (var codeFileUsing in codeFile.Usings)
         {
            codeFileTextStringBuilder.Append(codeFileUsing.UsingString);
         }

         foreach (var codeFileNamespace in codeFile.Namespaces)
         {
            codeFileTextStringBuilder.Append(codeFileNamespace.Definition);
         }

         string codeFileText = codeFileTextStringBuilder.ToString();
         return codeFileText;
      }

      #endregion
   }
}