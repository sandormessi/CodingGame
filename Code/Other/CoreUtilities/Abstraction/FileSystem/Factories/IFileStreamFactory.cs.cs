namespace CoreUtilities.Abstraction.FileSystem.Factories;

using System.IO;

public interface IFileStreamFactory
{
    #region Public Methods and Operators

    Stream CreateFileStream(string filePath, FileMode mode, FileAccess access, FileShare share);

    Stream CreateFileStream(string filePath, FileMode mode, FileAccess access);

    Stream CreateFileStream(string filePath, FileMode mode);

    Stream CreateFileStream(string filePath);

    #endregion
}