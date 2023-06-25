namespace CodeFileMergeApplication.Core.Abstraction.Logic;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using CodeFileMergeApplication.Core.Abstraction.Data;

public interface IUsingCollector
{
   #region Public Methods and Operators

   IEnumerable<Using> GetAllCodeFileUsing(IEnumerable<CodeFile> namespaces);

   Task<IEnumerable<Using>> GetAllCodeFileUsingAsync(IEnumerable<CodeFile> namespaces);

   Task<IEnumerable<Using>> GetAllCodeFileUsingAsync(IEnumerable<CodeFile> namespaces, CancellationToken cancellationToken);

   IEnumerable<Using> GetAllUsing(IEnumerable<Namespace> namespaces);

   Task<IEnumerable<Using>> GetAllUsingAsync(IEnumerable<Namespace> namespaces);

   Task<IEnumerable<Using>> GetAllUsingAsync(IEnumerable<Namespace> namespaces, CancellationToken cancellationToken);

   #endregion
}