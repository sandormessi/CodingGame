namespace CodeFileMergeApplication.Core.Abstraction.Logic;

using CodeFileMergeApplication.Core.Abstraction.Data;

public interface INamespaceExtractor
{
   #region Public Methods and Operators

   Namespace GetNamespace(string codeBlockText);

   #endregion
}