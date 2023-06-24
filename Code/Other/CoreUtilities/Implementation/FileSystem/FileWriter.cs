namespace CoreUtilities.Implementation.FileSystem;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CoreUtilities.Abstraction.FileSystem;
using CoreUtilities.Abstraction.FileSystem.Factories;

public class FileWriter : IFileWriter
{
   #region Constants and Fields

   private static readonly Encoding DefaultEncoding = Encoding.UTF8;

   private readonly IFileStreamFactory fileStreamFactory;

   private readonly ITextWriterFactory textWriterFactory;

   #endregion

   #region Constructors and Destructors

   public FileWriter(IFileStreamFactory fileStreamFactory, ITextWriterFactory textWriterFactory)
   {
      this.fileStreamFactory = fileStreamFactory ?? throw new ArgumentNullException(nameof(fileStreamFactory));
      this.textWriterFactory = textWriterFactory ?? throw new ArgumentNullException(nameof(textWriterFactory));
   }

   #endregion

   #region IFileWriter Members

   public void WriteAllText(string text, string filePath, Encoding encoding)
   {
      if (encoding == null)
      {
         throw new ArgumentNullException(nameof(encoding));
      }

      if (string.IsNullOrWhiteSpace(text))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(text));
      }

      if (string.IsNullOrWhiteSpace(filePath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
      }

      using var encodedStreamWriter = CreateEncodedStreamWriter(filePath, encoding);

      encodedStreamWriter.Write(text);
   }

   public void WriteAllText(string text, string filePath)
   {
      WriteAllText(text, filePath, DefaultEncoding);
   }

   public void WriteAllBytes(IReadOnlyList<byte> bytesToWrite, string filePath)
   {
      if (bytesToWrite == null)
      {
         throw new ArgumentNullException(nameof(bytesToWrite));
      }

      if (filePath == null)
      {
         throw new ArgumentNullException(nameof(filePath));
      }

      using var fileStream = CreateFileStream(filePath);

      fileStream.Write(bytesToWrite.ToArray());
   }

   public Task WriteAllBytesAsync(IReadOnlyList<byte> bytesToWrite, string filePath)
   {
      return WriteAllBytesAsync(bytesToWrite, filePath, CancellationToken.None);
   }

   public async Task WriteAllBytesAsync(IReadOnlyList<byte> bytesToWrite, string filePath, CancellationToken cancellationToken)
   {
      await using var fileStream = CreateFileStream(filePath);

      await fileStream.WriteAsync(bytesToWrite.ToArray(), cancellationToken).ConfigureAwait(false);
   }

   public Task WriteAllTextAsync(string text, string filePath, Encoding encoding)
   {
      return WriteAllTextAsync(text, filePath, encoding, CancellationToken.None);
   }

   public Task WriteAllTextAsync(string text, string filePath)
   {
      return WriteAllTextAsync(text, filePath, DefaultEncoding, CancellationToken.None);
   }

   public async Task WriteAllTextAsync(string text, string filePath, Encoding encoding, CancellationToken cancellationToken)
   {
      if (encoding == null)
      {
         throw new ArgumentNullException(nameof(encoding));
      }

      if (string.IsNullOrWhiteSpace(text))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(text));
      }

      if (string.IsNullOrWhiteSpace(filePath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
      }

      await using var encodedStreamWriter = CreateEncodedStreamWriter(filePath, encoding);

      await encodedStreamWriter.WriteAsync(text).ConfigureAwait(false);
   }

   public Task WriteAllTextAsync(string text, string filePath, CancellationToken cancellationToken)
   {
      return WriteAllTextAsync(text, filePath, DefaultEncoding, cancellationToken);
   }

   #endregion

   #region Methods

   private TextWriter CreateEncodedStreamWriter(string filePath, Encoding encoding)
   {
      var fileStream = CreateFileStream(filePath);
      var encodedStreamWriter = textWriterFactory.CreateTextWriter(fileStream, encoding, false);
      return encodedStreamWriter;
   }

   private Stream CreateFileStream(string filePath)
   {
      var fileStream = fileStreamFactory.CreateFileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
      return fileStream;
   }

   #endregion
}