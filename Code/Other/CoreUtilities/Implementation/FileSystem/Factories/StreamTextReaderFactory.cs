namespace CoreUtilities.Implementation.FileSystem.Factories;

using System.IO;
using System.Text;

using CoreUtilities.Abstraction.FileSystem.Factories;

public class StreamTextReaderFactory : IStreamTextReaderFactory
{
   #region Constants and Fields

   private static readonly Encoding DefaultEncoding = Encoding.UTF8;

   private const bool DefaultLeaveOpen = false;

   #endregion

   #region IStreamTextReaderFactory Members

   public TextReader CreateTextReader(Stream baseStream, Encoding encoding)
   {
      return CreateTextReader(baseStream, encoding, DefaultLeaveOpen);
   }

   public TextReader CreateTextReader(Stream baseStream)
   {
      return CreateTextReader(baseStream, DefaultEncoding);
   }

   public TextReader CreateTextReader(Stream baseStream, Encoding encoding, bool leaveOpen)
   {
      return new StreamReader(baseStream, encoding, leaveOpen);
   }

   public TextReader CreateTextReader(Stream baseStream, bool leaveOpen)
   {
      return CreateTextReader(baseStream, DefaultEncoding, leaveOpen);
   }

   #endregion
}