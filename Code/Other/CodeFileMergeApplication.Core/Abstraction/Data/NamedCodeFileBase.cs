namespace CodeFileMergeApplication.Core.Abstraction.Data;

using System;

public class NamedCodeFileBase
{
   #region Constructors and Destructors

   public NamedCodeFileBase(string name, string content)
   {
      if (string.IsNullOrWhiteSpace(name))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
      }

      if (string.IsNullOrWhiteSpace(content))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(content));
      }

      Name = name;
      Content = content;
   }

   #endregion

   #region Public Properties

   public string Content { get; }

   public string Name { get; }

   #endregion
}