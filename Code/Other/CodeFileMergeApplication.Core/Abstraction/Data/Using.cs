namespace CodeFileMergeApplication.Core.Abstraction.Data;

using System;

public class Using
{
   #region Constructors and Destructors

   public Using(string usingString)
   {
      if (string.IsNullOrWhiteSpace(usingString))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(usingString));
      }

      UsingString = usingString;
   }

   #endregion

   #region Public Properties

   public string UsingString { get; }

   #endregion
}