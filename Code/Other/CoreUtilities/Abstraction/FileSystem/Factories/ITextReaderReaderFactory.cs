namespace CoreUtilities.Abstraction.FileSystem.Factories;

using System.IO;
using System.Text;

public interface ITextReaderReaderFactory
{
    #region Public Methods and Operators

    TextReader CreateTextReader(Stream baseStream, Encoding encoding);

    TextReader CreateTextReader(Stream baseStream);

    #endregion
}