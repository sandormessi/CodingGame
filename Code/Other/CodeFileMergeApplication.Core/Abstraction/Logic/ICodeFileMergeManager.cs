namespace CodeFileMergeApplication.Core.Abstraction.Logic;

using System.Threading;
using System.Threading.Tasks;

using CodeFileMergeApplication.Core.Abstraction.Data;

public interface ICodeFileMergeManager
{
   #region Public Methods and Operators

   CodeFile MergeCodeFiles();

   Task<CodeFile> MergeCodeFilesAsync();

   Task<CodeFile> MergeCodeFilesAsync(CancellationToken cancellationToken);

   void WriteCodeFile(CodeFile codeFile, string targetPath);

   Task WriteCodeFileAsync(CodeFile codeFile, string targetPath);

   Task WriteCodeFileAsync(CodeFile codeFile, string targetPath, CancellationToken cancellationToken);

   #endregion
}