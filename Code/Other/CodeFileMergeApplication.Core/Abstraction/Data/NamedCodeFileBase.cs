namespace CodeFileMergeApplication.Core.Abstraction.Data;

using System;

public class NamedCodeFileBase
{
   #region Constructors and Destructors

   public NamedCodeFileBase(string name, string header, string body)
   {
      if (string.IsNullOrWhiteSpace(name))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
      }

      if (string.IsNullOrWhiteSpace(header))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(header));
      }

      if (string.IsNullOrWhiteSpace(body))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(body));
      }

      Name = name;
      Header = header;
      Body = body;
   }

   #endregion

   #region Public Properties

   public string Body { get; }

   public string Header { get; }

   public string Name { get; }

   #endregion
}