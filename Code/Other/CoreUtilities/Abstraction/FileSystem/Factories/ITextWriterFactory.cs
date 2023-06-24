namespace CoreUtilities.Abstraction.FileSystem.Factories;

using System.IO;
using System.Text;

public interface ITextWriterFactory
{
   #region Public Methods and Operators

   TextWriter CreateTextWriter(Stream baseStream, Encoding encoding);

   TextWriter CreateTextWriter(Stream baseStream);

   #endregion
}