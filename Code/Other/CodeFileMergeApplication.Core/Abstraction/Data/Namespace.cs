namespace CodeFileMergeApplication.Core.Abstraction.Data;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class Namespace
{
   #region Constructors and Destructors

   public Namespace(string name, string body, IEnumerable<Namespace> namespaces, IReadOnlyList<TypeDeclaration> types, IReadOnlyList<Using> usings)
   {
      if (string.IsNullOrWhiteSpace(name))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
      }

      if (string.IsNullOrWhiteSpace(body))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(body));
      }

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

      Name = name;
      Body = body;
      Usings = new ReadOnlyCollection<Using>(usings.ToArray());
      Types = new ReadOnlyCollection<TypeDeclaration>(types.ToArray());
      Namespaces = new ReadOnlyCollection<Namespace>(namespaces.ToArray());
   }

   #endregion

   #region Public Properties

   public string Body { get; }

   public string Name { get; }

   public IReadOnlyList<Namespace> Namespaces { get; }

   public IReadOnlyList<TypeDeclaration> Types { get; }

   public IReadOnlyList<Using> Usings { get; }

   #endregion
}