namespace CoreUtilities.Implementation.FileSystem.Factories;

using System;
using System.IO;
using System.Text;

using CoreUtilities.Abstraction.FileSystem.Factories;

public class TextWriterFactory : ITextWriterFactory
{
   #region ITextWriterFactory Members

   public TextWriter CreateTextWriter(Stream baseStream, Encoding encoding)
   {
      if (baseStream == null)
      {
         throw new ArgumentNullException(nameof(baseStream));
      }

      if (encoding == null)
      {
         throw new ArgumentNullException(nameof(encoding));
      }

      return new StreamWriter(baseStream, encoding);
   }

   public TextWriter CreateTextWriter(Stream baseStream)
   {
      return CreateTextWriter(baseStream, Encoding.UTF8);
   }

   #endregion
}