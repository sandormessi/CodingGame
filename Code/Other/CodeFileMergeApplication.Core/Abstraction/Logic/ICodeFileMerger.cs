namespace CodeFileMergeApplication.Core.Abstraction.Logic;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using CodeFileMergeApplication.Core.Abstraction.Data;

public interface ICodeFileMerger
{
   #region Public Methods and Operators

   CodeFile MergeCodeFiles(IEnumerable<CodeFile> codeFilesToMerge);

   Task<CodeFile> MergeCodeFilesAsync(IEnumerable<CodeFile> codeFilesToMerge);

   Task<CodeFile> MergeCodeFilesAsync(IEnumerable<CodeFile> codeFilesToMerge, CancellationToken cancellationToken);

   #endregion
}