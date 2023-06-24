﻿namespace CoreUtilities.Implementation.FileSystem;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CoreUtilities.Abstraction.FileSystem;
using CoreUtilities.Abstraction.FileSystem.Factories;

public class FileReader : IFileReader
{
   #region Constants and Fields

   private const int BufferSize = 1024 * 1024 * 16;

   private readonly IFileStreamFactory fileStreamFactory;

   private readonly ITextReaderReaderFactory textReaderReaderFactory;

   #endregion

   #region Constructors and Destructors

   public FileReader(IFileStreamFactory fileStreamFactory, ITextReaderReaderFactory textReaderReaderFactory)
   {
      this.fileStreamFactory = fileStreamFactory ?? throw new ArgumentNullException(nameof(fileStreamFactory));
      this.textReaderReaderFactory = textReaderReaderFactory ?? throw new ArgumentNullException(nameof(textReaderReaderFactory));
   }

   #endregion

   #region IFileReader Members

   public IReadOnlyList<byte> ReadAllBytesFromFile(string filePath)
   {
      if (string.IsNullOrWhiteSpace(filePath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
      }

      using var fileStream = CreateFileStream(filePath);
      var buffer = new byte[BufferSize];

      List<byte> bytesRead = new((int)fileStream.Length);

      var byteRead = fileStream.Read(buffer);

      while (byteRead > 0)
      {
         bytesRead.AddRange(buffer);
         byteRead = fileStream.Read(buffer);
      }

      return new ReadOnlyCollection<byte>(bytesRead);
   }

   public Task<IReadOnlyList<byte>> ReadAllBytesFromFileAsync(string filePath)
   {
      if (string.IsNullOrWhiteSpace(filePath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
      }

      return ReadAllBytesFromFileAsync(filePath, CancellationToken.None);
   }

   public async Task<IReadOnlyList<byte>> ReadAllBytesFromFileAsync(string filePath, CancellationToken cancellationToken)
   {
      if (string.IsNullOrWhiteSpace(filePath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
      }

      await using var fileStream = CreateFileStream(filePath);
      var buffer = new byte[BufferSize];

      List<byte> bytesRead = new((int)fileStream.Length);

      var byteRead = await fileStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);

      while (byteRead > 0)
      {
         bytesRead.AddRange(buffer);
         byteRead = await fileStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
      }

      return new ReadOnlyCollection<byte>(bytesRead);
   }

   public string ReadAllTextFromFile(string filePath)
   {
      return ReadAllTextFromFile(filePath, Encoding.UTF8);
   }

   public string ReadAllTextFromFile(string filePath, Encoding encoding)
   {
      if (encoding == null)
      {
         throw new ArgumentNullException(nameof(encoding));
      }

      if (string.IsNullOrWhiteSpace(filePath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
      }

      using var encodedFileReader = CreateEncodedFileReader(filePath, encoding);

      var content = encodedFileReader.ReadToEnd();

      return content;
   }

   public Task<string> ReadAllTextFromFileAsync(string filePath)
   {
      return ReadAllTextFromFileAsync(filePath, Encoding.UTF8, CancellationToken.None);
   }

   public Task<string> ReadAllTextFromFileAsync(string filePath, CancellationToken cancellationToken)
   {
      return ReadAllTextFromFileAsync(filePath, Encoding.UTF8, cancellationToken);
   }

   public Task<string> ReadAllTextFromFileAsync(string filePath, Encoding encoding)
   {
      return ReadAllTextFromFileAsync(filePath, Encoding.UTF8, CancellationToken.None);
   }

   public async Task<string> ReadAllTextFromFileAsync(string filePath, Encoding encoding, CancellationToken cancellationToken)
   {
      if (encoding == null)
      {
         throw new ArgumentNullException(nameof(encoding));
      }

      if (string.IsNullOrWhiteSpace(filePath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
      }

      using var encodedFileReader = CreateEncodedFileReader(filePath, encoding);

      var content = await encodedFileReader.ReadToEndAsync().ConfigureAwait(false);

      return content;
   }

   #endregion

   #region Methods

   private TextReader CreateEncodedFileReader(string filePath, Encoding encoding)
   {
      var fileStream = CreateFileStream(filePath);
      var encodedFileReader = textReaderReaderFactory.CreateTextReader(fileStream, encoding);
      return encodedFileReader;
   }

   private Stream CreateFileStream(string filePath)
   {
      var fileStream = fileStreamFactory.CreateFileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
      return fileStream;
   }

   #endregion
}