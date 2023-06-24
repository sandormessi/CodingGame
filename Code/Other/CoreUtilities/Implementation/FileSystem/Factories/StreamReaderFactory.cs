namespace CoreUtilities.Implementation.FileSystem.Factories;

using System;
using System.IO;
using System.Text;
using CoreUtilities.Abstraction.FileSystem.Factories;

public class StreamReaderFactory : IStreamReaderFactory
{
    public TextReader CreateReader(Stream baseStream, Encoding encoding)
    {
        if (baseStream == null)
        {
            throw new ArgumentNullException(nameof(baseStream));
        }

        if (encoding == null)
        {
            throw new ArgumentNullException(nameof(encoding));
        }

        return new StreamReader(baseStream, encoding);
    }

    public TextReader CreateReader(Stream baseStream)
    {
        return CreateReader(baseStream, Encoding.UTF8);
    }
}