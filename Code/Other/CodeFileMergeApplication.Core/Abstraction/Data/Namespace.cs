namespace CodeFileMergeApplication.Core.Abstraction.Data;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class Namespace : NamedCodeFileBase
{
   #region Constructors and Destructors

   public Namespace(string name, string header, string body, IEnumerable<Namespace> namespaces, IReadOnlyList<TypeDeclaration> types,
      IReadOnlyList<Using> usings)
      : base(name, header, body)
   {
      if (namespaces == null)
      {
         throw new ArgumentNullException(nameof(namespaces));
      }

      if (types == null)
      {
         throw new ArgumentNullException(nameof(types));
      }

      if (usings == null)
      {
         throw new ArgumentNullException(nameof(usings));
      }

      Usings = new ReadOnlyCollection<Using>(usings.ToArray());
      Types = new ReadOnlyCollection<TypeDeclaration>(types.ToArray());
      Namespaces = new ReadOnlyCollection<Namespace>(namespaces.ToArray());
   }

   #endregion

   #region Public Properties

   public IReadOnlyList<Namespace> Namespaces { get; }

   public IReadOnlyList<TypeDeclaration> Types { get; }

   public IReadOnlyList<Using> Usings { get; }

   #endregion
}