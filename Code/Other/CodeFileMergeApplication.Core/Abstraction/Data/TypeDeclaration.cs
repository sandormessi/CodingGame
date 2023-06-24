namespace CodeFileMergeApplication.Core.Abstraction.Data;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class TypeDeclaration
{
   #region Constructors and Destructors

   public TypeDeclaration(string name, string body, IEnumerable<TypeDeclaration> embeddedTypes)
   {
      if (string.IsNullOrWhiteSpace(name))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
      }

      if (string.IsNullOrWhiteSpace(body))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(body));
      }

      if (embeddedTypes == null)
      {
         throw new ArgumentNullException(nameof(embeddedTypes));
      }

      Name = name;
      Body = body;
      EmbeddedTypes = new ReadOnlyCollection<TypeDeclaration>(embeddedTypes.ToArray());
   }

   #endregion

   #region Public Properties

   public string Body { get; }

   public IReadOnlyList<TypeDeclaration> EmbeddedTypes { get; }

   public string Name { get; }

   #endregion
}