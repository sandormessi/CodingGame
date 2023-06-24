namespace CoreUtilities.Abstraction;

using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public interface IFileReader
{
   #region Public Methods and Operators

   IReadOnlyList<byte> ReadAllBytesFromFile(string filePath);

   Task<IReadOnlyList<byte>> ReadAllBytesFromFileAsync(string filePath);

   Task<IReadOnlyList<byte>> ReadAllBytesFromFileAsync(string filePath, CancellationToken cancellationToken);

   string ReadAllTextFromFile(string filePath);

   string ReadAllTextFromFile(string filePath, Encoding encoding);

   Task<string> ReadAllTextFromFileAsync(string filePath);

   Task<string> ReadAllTextFromFileAsync(string filePath, CancellationToken cancellationToken);

   Task<string> ReadAllTextFromFileAsync(string filePath, Encoding encoding);

   Task<string> ReadAllTextFromFileAsync(string filePath, Encoding encoding, CancellationToken cancellationToken);

   #endregion
}