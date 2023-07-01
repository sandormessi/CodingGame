namespace CodeFileMergeApplication.Core.Abstraction.Data;

using System;

public class NamedCodeFileBase
{
   #region Constructors and Destructors

   protected NamedCodeFileBase(string name, string definition)
   {
      if (string.IsNullOrWhiteSpace(name))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
      }

      if (string.IsNullOrWhiteSpace(definition))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(definition));
      }

      Name = name;
      Definition = definition;
   }

   #endregion

   #region Public Properties

   public string Definition { get; }

   public string Name { get; }

   #endregion
}