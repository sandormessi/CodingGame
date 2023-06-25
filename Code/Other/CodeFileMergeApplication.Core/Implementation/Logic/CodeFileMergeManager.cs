namespace CodeFileMergeApplication.Core.Implementation.Logic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CodeFileMergeApplication.Core.Abstraction.Data;
using CodeFileMergeApplication.Core.Abstraction.Logic;

public class CodeFileMergeManager : ICodeFileMergeManager
{
   #region Constants and Fields

   private readonly ICodeFileMerger codeFileMerger;

   private readonly ICodeFilePathProvider codeFilePathProvider;

   private readonly ICodeFileReader codeFileReader;

   private readonly ICodeFileWriter codeFileWriter;

   #endregion

   #region Constructors and Destructors

   public CodeFileMergeManager(ICodeFileReader codeFileReader, ICodeFileMerger codeFileMerger, ICodeFilePathProvider codeFilePathProvider,
      ICodeFileWriter codeFileWriter)
   {
      this.codeFileReader = codeFileReader ?? throw new ArgumentNullException(nameof(codeFileReader));
      this.codeFileMerger = codeFileMerger ?? throw new ArgumentNullException(nameof(codeFileMerger));
      this.codeFilePathProvider = codeFilePathProvider ?? throw new ArgumentNullException(nameof(codeFilePathProvider));
      this.codeFileWriter = codeFileWriter ?? throw new ArgumentNullException(nameof(codeFileWriter));
   }

   #endregion

   #region ICodeFileMergeManager Members

   public CodeFile MergeCodeFiles()
   {
      var paths = codeFilePathProvider.GetCodeFilePaths();
      var codeFiles = paths.Select(codeFileReader.ReadFromFile);
      var mergedCodeFile = codeFileMerger.MergeCodeFiles(codeFiles);

      return mergedCodeFile;
   }

   public Task<CodeFile> MergeCodeFilesAsync()
   {
      return MergeCodeFilesAsync(CancellationToken.None);
   }

   public async Task<CodeFile> MergeCodeFilesAsync(CancellationToken cancellationToken)
   {
      var paths = codeFilePathProvider.GetCodeFilePaths();

      List<CodeFile> codeFiles = new();
      foreach (var path in paths)
      {
         var codeFile = await codeFileReader.ReadFromFileAsync(path, cancellationToken);
         codeFiles.Add(codeFile);
      }

      return await codeFileMerger.MergeCodeFilesAsync(codeFiles, cancellationToken).ConfigureAwait(false);
   }

   public void WriteCodeFile(CodeFile codeFile, string targetPath)
   {
      codeFileWriter.WriteCodeFile(codeFile, targetPath);
   }

   public Task WriteCodeFileAsync(CodeFile codeFile, string targetPath)
   {
      return WriteCodeFileAsync(codeFile, targetPath, CancellationToken.None);
   }

   public Task WriteCodeFileAsync(CodeFile codeFile, string targetPath, CancellationToken cancellationToken)
   {
      if (codeFile == null)
      {
         throw new ArgumentNullException(nameof(codeFile));
      }

      if (string.IsNullOrWhiteSpace(targetPath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(targetPath));
      }

      return codeFileWriter.WriteCodeFileAsync(codeFile, targetPath, cancellationToken);
   }

   #endregion
}