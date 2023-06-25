namespace CoreUtilities.Implementation.FileSystem.Factories;

using System.IO;
using System.Text;

using CoreUtilities.Abstraction.FileSystem.Factories;

public class StreamTextWriterFactory : IStreamTextWriterFactory
{
   #region Constants and Fields

   private const int DefaultBufferSize = 4096;

   private const bool DefaultLeaveOpen = false;

   private static readonly Encoding DefaultEncoding = Encoding.UTF8;

   #endregion

   #region IStreamTextWriterFactory Members

   public TextWriter CreateTextWriter(Stream baseStream, Encoding encoding)
   {
      return CreateTextWriter(baseStream, encoding, DefaultLeaveOpen);
   }

   public TextWriter CreateTextWriter(Stream baseStream)
   {
      return CreateTextWriter(baseStream, DefaultEncoding, DefaultLeaveOpen);
   }

   public TextWriter CreateTextWriter(Stream baseStream, Encoding encoding, bool leaveOpen)
   {
      return new StreamWriter(baseStream, encoding, DefaultBufferSize, leaveOpen);
   }

   public TextWriter CreateTextWriter(Stream baseStream, bool leaveOpen)
   {
      return CreateTextWriter(baseStream, DefaultEncoding, leaveOpen);
   }

   #endregion
}