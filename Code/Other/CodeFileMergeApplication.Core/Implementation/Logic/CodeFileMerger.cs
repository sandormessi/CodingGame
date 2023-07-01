namespace CodeFileMergeApplication.Core.Implementation.Logic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CodeFileMergeApplication.Core.Abstraction.Data;
using CodeFileMergeApplication.Core.Abstraction.Logic;

public class CodeFileMerger : ICodeFileMerger
{
   #region Constants and Fields

   private const int InitialStringBuilderCapacity = 1024 * 16;

   private readonly IUsingCollector usingCollector;

   #endregion

   #region Constructors and Destructors

   public CodeFileMerger(IUsingCollector usingCollector)
   {
      this.usingCollector = usingCollector ?? throw new ArgumentNullException(nameof(usingCollector));
   }

   #endregion

   #region ICodeFileMerger Members

   public CodeFile MergeCodeFiles(IEnumerable<CodeFile> codeFilesToMerge)
   {
      if (codeFilesToMerge == null)
      {
         throw new ArgumentNullException(nameof(codeFilesToMerge));
      }

      var filesToMerge = codeFilesToMerge as CodeFile[] ?? codeFilesToMerge.ToArray();
      var usings = CollectUsings(filesToMerge);
      var namespaces = CollectNamespaces(filesToMerge);
      var codeFileText = CreateFileContent(usings, namespaces);

      return new CodeFile("<Unknown>", codeFileText, namespaces, usings);
   }

   public Task<CodeFile> MergeCodeFilesAsync(IEnumerable<CodeFile> codeFilesToMerge)
   {
      return MergeCodeFilesAsync(codeFilesToMerge, CancellationToken.None);
   }

   public Task<CodeFile> MergeCodeFilesAsync(IEnumerable<CodeFile> codeFilesToMerge, CancellationToken cancellationToken)
   {
      if (codeFilesToMerge == null)
      {
         throw new ArgumentNullException(nameof(codeFilesToMerge));
      }

      return Task.Factory.StartNew(() => MergeCodeFiles(codeFilesToMerge), cancellationToken);
   }

   #endregion

   #region Methods

   private static Namespace[] CollectNamespaces(IEnumerable<CodeFile> filesToMerge)
   {
      return filesToMerge.SelectMany(x => x.Namespaces).Select(CollectNamespaces).SelectMany(x => x).ToArray();
   }

   private static IEnumerable<Namespace> CollectNamespaces(Namespace arg)
   {
      IEnumerable<Namespace> nestedNamespaces = arg.Namespaces;

      var actualLevel = nestedNamespaces;
      while (actualLevel.Any())
      {
         actualLevel = nestedNamespaces.SelectMany(x => x.Namespaces).ToArray();
         nestedNamespaces = nestedNamespaces.Union(actualLevel).ToArray();
      }

      return nestedNamespaces;
   }

   private static string CreateFileContent(IEnumerable<Using> usings, IEnumerable<Namespace> namespaces)
   {
      StringBuilder codeFileTextStringBuilder = new(InitialStringBuilderCapacity);

      foreach (var codeFileUsing in usings)
      {
         codeFileTextStringBuilder.Append(codeFileUsing.UsingString);
      }

      foreach (var codeFileNamespace in namespaces)
      {
         codeFileTextStringBuilder.Append(codeFileNamespace.Definition);
      }

      string codeFileText = codeFileTextStringBuilder.ToString();
      return codeFileText;
   }

   private Using[] CollectUsings(IEnumerable<CodeFile> filesToMerge)
   {
      return usingCollector.GetAllCodeFileUsing(filesToMerge).ToArray();
   }

   #endregion
}