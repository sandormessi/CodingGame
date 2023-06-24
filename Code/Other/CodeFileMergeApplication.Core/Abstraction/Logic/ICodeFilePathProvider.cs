namespace CodeFileMergeApplication.Core.Abstraction.Logic;

using System.Collections.Generic;

public interface ICodeFilePathProvider
{
   #region Public Methods and Operators

   IEnumerable<string> GetCodeFilePaths();

   #endregion
}