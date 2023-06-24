namespace CodeFileMergeApplication.Core.Implementation.Logic;

using System;
using System.Threading;
using System.Threading.Tasks;

using CodeFileMergeApplication.Core.Abstraction.Data;
using CodeFileMergeApplication.Core.Abstraction.Logic;

using CoreUtilities.Abstraction.FileSystem;

public class CodeFileReader : ICodeFileReader
{
   #region Constants and Fields

   private readonly IFileReader fileReader;

   #endregion

   #region Constructors and Destructors

   public CodeFileReader(IFileReader fileReader)
   {
      this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
   }

   #endregion

   #region ICodeFileReader Members

   public CodeFile ReadFromFile(string filePath)
   {
      if (string.IsNullOrWhiteSpace(filePath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
      }

      var fileContent = fileReader.ReadAllTextFromFile(filePath);

      return ReadFromString(fileContent);
   }

   public CodeFile ReadFromString(string fileContent)
   {
      if (string.IsNullOrWhiteSpace(fileContent))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileContent));
      }

      throw new NotImplementedException();
   }

   public Task<CodeFile> ReadFromStringAsync(string fileContent, CancellationToken cancellationToken)
   {
      if (string.IsNullOrWhiteSpace(fileContent))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileContent));
      }

      return Task.Factory.StartNew(() => ReadFromString(fileContent), cancellationToken);
   }

   public Task<CodeFile> ReadFromFileAsync(string filePath)
   {
      return ReadFromFileAsync(filePath, CancellationToken.None);
   }

   public Task<CodeFile> ReadFromStringAsync(string fileContent)
   {
      return ReadFromStringAsync(fileContent, CancellationToken.None);
   }

   public async Task<CodeFile> ReadFromFileAsync(string filePath, CancellationToken cancellationToken)
   {
      if (string.IsNullOrWhiteSpace(filePath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
      }

      var fileContent = await fileReader.ReadAllTextFromFileAsync(filePath, cancellationToken).ConfigureAwait(false);

      return await ReadFromStringAsync(fileContent, cancellationToken).ConfigureAwait(false);
   }

   #endregion
}