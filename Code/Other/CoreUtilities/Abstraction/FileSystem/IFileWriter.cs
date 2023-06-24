namespace CoreUtilities.Abstraction.FileSystem;

using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public interface IFileWriter
{
   #region Public Methods and Operators

   void WriteAllBytes(IReadOnlyList<byte> bytesToWrite, string filePath, Encoding encoding);

   Task WriteAllBytesAsync(IReadOnlyList<byte> bytesToWrite, string filePath, Encoding encoding);

   Task WriteAllBytesAsync(IReadOnlyList<byte> bytesToWrite, string filePath, Encoding encoding, CancellationToken cancellationToken);

   void WriteAllText(string text, string filePath, Encoding encoding);

   void WriteAllText(string text, string filePath);

   Task WriteAllTextAsync(string text, string filePath, Encoding encoding);

   Task WriteAllTextAsync(string text, string filePath);

   Task WriteAllTextAsync(string text, string filePath, Encoding encoding, CancellationToken cancellationToken);

   Task WriteAllTextAsync(string text, string filePath, CancellationToken cancellationToken);

   #endregion
}