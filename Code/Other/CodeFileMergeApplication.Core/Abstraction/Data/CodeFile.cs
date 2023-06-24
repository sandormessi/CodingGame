namespace CodeFileMergeApplication.Core.Abstraction.Data;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

public class CodeFile
{
   #region Constructors and Destructors

   public CodeFile(string filePath, string content, IEnumerable<Namespace> namespaces, IEnumerable<Using> usings)
   {
      if (string.IsNullOrWhiteSpace(filePath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
      }

      if (string.IsNullOrWhiteSpace(content))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(content));
      }

      if (namespaces == null)
      {
         throw new ArgumentNullException(nameof(namespaces));
      }

      if (usings == null)
      {
         throw new ArgumentNullException(nameof(usings));
      }

      FilePath = filePath;
      Namespaces = new ReadOnlyCollection<Namespace>(namespaces.ToArray());
      Content = content;
      Usings = new ReadOnlyCollection<Using>(usings.ToArray());
      FileName = Path.GetFileNameWithoutExtension(filePath);
   }

   #endregion

   #region Public Properties

   public string Content { get; }

   public string FileName { get; }

   public string FilePath { get; }

   public IReadOnlyList<Namespace> Namespaces { get; }

   public IReadOnlyList<Using> Usings { get; }

   #endregion
}