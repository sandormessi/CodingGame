namespace CoreUtilities.Abstraction.FileSystem.Factories;

using System.IO;
using System.Text;

public interface IStreamTextReaderFactory
{
   #region Public Methods and Operators

   TextReader CreateTextReader(Stream baseStream, Encoding encoding);

   TextReader CreateTextReader(Stream baseStream);

   TextReader CreateTextReader(Stream baseStream, Encoding encoding, bool leaveOpen);

   TextReader CreateTextReader(Stream baseStream, bool leaveOpen);

   #endregion
}