namespace CodeFileMergeApplication.Core.Abstraction.Logic;

using System.Threading;
using System.Threading.Tasks;

using CodeFileMergeApplication.Core.Abstraction.Data;

public interface ICodeFileReader
{
   #region Public Methods and Operators

   CodeFile ReadFromFile(string filePath);

   Task<CodeFile> ReadFromFileAsync(string filePath);

   Task<CodeFile> ReadFromFileAsync(string filePath, CancellationToken cancellationToken);

   CodeFile ReadFromString(string fileContent);

   Task<CodeFile> ReadFromStringAsync(string fileContent);

   Task<CodeFile> ReadFromStringAsync(string fileContent, CancellationToken cancellationToken);

   #endregion
}