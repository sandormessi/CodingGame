namespace CoreUtilities.Abstraction.FileSystem.Factories;

using System.IO;
using System.Text;

public interface IStreamTextWriterFactory
{
   #region Public Methods and Operators

   TextWriter CreateTextWriter(Stream baseStream, Encoding encoding);

   TextWriter CreateTextWriter(Stream baseStream);

   TextWriter CreateTextWriter(Stream baseStream, Encoding encoding, bool leaveOpen);

   TextWriter CreateTextWriter(Stream baseStream, bool leaveOpen);

   #endregion
}