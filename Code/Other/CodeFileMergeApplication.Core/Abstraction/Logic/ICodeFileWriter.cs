namespace CodeFileMergeApplication.Core.Abstraction.Logic;

using System.Threading;
using System.Threading.Tasks;

using CodeFileMergeApplication.Core.Abstraction.Data;

public interface ICodeFileWriter
{
   #region Public Methods and Operators

   void WriteCodeFile(CodeFile codeFile, string targetPath);

   Task WriteCodeFileAsync(CodeFile codeFile, string targetPath);

   Task WriteCodeFileAsync(CodeFile codeFile, string targetPath, CancellationToken cancellationToken);

   #endregion
}