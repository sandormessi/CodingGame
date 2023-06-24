namespace CoreUtilities.Implementation.FileSystem;

using System.IO;

using CoreUtilities.Abstraction.FileSystem;

public class FileStreamFactory : IFileStreamFactory
{
   #region IFileStreamFactory Members

   public Stream CreateFileStream(string filePath, FileMode mode, FileAccess access, FileShare share)
   {
      return new FileStream(filePath, mode, access, share);
   }

   public Stream CreateFileStream(string filePath, FileMode mode, FileAccess access)
   {
      return CreateFileStream(filePath, mode, access, FileShare.None);
   }

   public Stream CreateFileStream(string filePath, FileMode mode)
   {
      return CreateFileStream(filePath, mode, FileAccess.Read, FileShare.None);
   }

   public Stream CreateFileStream(string filePath)
   {
      return CreateFileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
   }

   #endregion
}