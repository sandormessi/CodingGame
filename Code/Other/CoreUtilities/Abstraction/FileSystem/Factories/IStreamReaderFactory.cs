namespace CoreUtilities.Abstraction.FileSystem.Factories;

using System.IO;
using System.Text;

public interface IStreamReaderFactory
{
    #region Public Methods and Operators

    TextReader CreateReader(Stream baseStream, Encoding encoding);

    TextReader CreateReader(Stream baseStream);

    #endregion
}