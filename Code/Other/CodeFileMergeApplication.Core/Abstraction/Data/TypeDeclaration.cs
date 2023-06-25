namespace CodeFileMergeApplication.Core.Abstraction.Data;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class TypeDeclaration : NamedCodeFileBase
{
   #region Constructors and Destructors

   public TypeDeclaration(string name, string definition, IEnumerable<TypeDeclaration> embeddedTypes)
      : base(name, definition)
   {
      if (embeddedTypes == null)
      {
         throw new ArgumentNullException(nameof(embeddedTypes));
      }

      EmbeddedTypes = new ReadOnlyCollection<TypeDeclaration>(embeddedTypes.ToArray());
   }

   #endregion

   #region Public Properties

   public IReadOnlyList<TypeDeclaration> EmbeddedTypes { get; }

   #endregion
}