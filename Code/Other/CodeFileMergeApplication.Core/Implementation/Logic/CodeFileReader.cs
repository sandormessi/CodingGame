namespace CodeFileMergeApplication.Core.Implementation.Logic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CodeFileMergeApplication.Core.Abstraction.Data;
using CodeFileMergeApplication.Core.Abstraction.Logic;

using CoreUtilities.Abstraction.FileSystem;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public class CodeFileReader : ICodeFileReader
{
   #region Constants and Fields

   private const string UnknownFilePath = "<Unknown>";

   private readonly IFileReader fileReader;

   #endregion

   #region Constructors and Destructors

   public CodeFileReader(IFileReader fileReader)
   {
      this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
   }

   #endregion

   #region ICodeFileReader Members

   public CodeFile ReadFromFile(string filePath)
   {
      if (string.IsNullOrWhiteSpace(filePath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
      }

      var fileContent = fileReader.ReadAllTextFromFile(filePath);

      return ReadCodeFileFromString(filePath, fileContent);
   }

   public CodeFile ReadFromString(string fileContent)
   {
      if (string.IsNullOrWhiteSpace(fileContent))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileContent));
      }

      return ReadCodeFileFromString(fileContent, UnknownFilePath);
   }

   public Task<CodeFile> ReadFromStringAsync(string fileContent, CancellationToken cancellationToken)
   {
      if (string.IsNullOrWhiteSpace(fileContent))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileContent));
      }

      return ReadCodeFileFromStringAsync(fileContent, UnknownFilePath, cancellationToken);
   }

   public Task<CodeFile> ReadFromFileAsync(string filePath)
   {
      return ReadFromFileAsync(filePath, CancellationToken.None);
   }

   public Task<CodeFile> ReadFromStringAsync(string fileContent)
   {
      return ReadFromStringAsync(fileContent, CancellationToken.None);
   }

   public async Task<CodeFile> ReadFromFileAsync(string filePath, CancellationToken cancellationToken)
   {
      if (string.IsNullOrWhiteSpace(filePath))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
      }

      var fileContent = await fileReader.ReadAllTextFromFileAsync(filePath, cancellationToken).ConfigureAwait(false);

      return await ReadCodeFileFromStringAsync(fileContent, filePath, cancellationToken).ConfigureAwait(false);
   }

   #endregion

   #region Methods

   private static Using CreateUsingFromUsingDeclarationSyntax(UsingDirectiveSyntax arg)
   {
      var usingString = arg.GetText().ToString();
      return new Using(usingString);
   }

   private Namespace CreateNamespaceFromNamespaceDeclarationSyntax(NamespaceDeclarationSyntax namespaceDeclaration)
   {
      var name = namespaceDeclaration.Name.GetText().ToString();
      var definition = namespaceDeclaration.GetText().ToString();

      var memberDeclarations = namespaceDeclaration.Members;

      var namespaceDeclarations = memberDeclarations.OfType<NamespaceDeclarationSyntax>().ToArray();

      IEnumerable<Namespace> namespaces = namespaceDeclarations.Select(CreateNamespaceFromNamespaceDeclarationSyntax);

      var typeDeclarations = memberDeclarations.OfType<TypeDeclarationSyntax>().ToArray();

      IEnumerable<TypeDeclaration> types = typeDeclarations.Select(CreateTypeDeclarationFromTypeDeclarationSyntax);

      var usingDeclarations = namespaceDeclaration.Usings.ToArray();

      IEnumerable<Using> usings = usingDeclarations.Select(CreateUsingFromUsingDeclarationSyntax);

      return new Namespace(name, definition, namespaces, types, usings);
   }

   private TypeDeclaration CreateTypeDeclarationFromTypeDeclarationSyntax(TypeDeclarationSyntax arg)
   {
      var name = arg.Identifier.Text;
      var definition = arg.GetText().ToString();

      var nestedTypeDeclarations = arg.Members.OfType<TypeDeclarationSyntax>().ToArray();

      IEnumerable<TypeDeclaration> embeddedTypes = nestedTypeDeclarations.Select(CreateTypeDeclarationFromTypeDeclarationSyntax);

      return new TypeDeclaration(name, definition, embeddedTypes);
   }

   private CodeFile ReadCodeFileFromString(string filePath, string fileContent)
   {
      SyntaxTree tree = CSharpSyntaxTree.ParseText(fileContent);
      CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

      var namespaceDeclarations = root.Members.OfType<NamespaceDeclarationSyntax>().ToArray();
      var usingDeclarations = root.Usings;

      var namespaces = namespaceDeclarations.Select(CreateNamespaceFromNamespaceDeclarationSyntax);

      IEnumerable<Using> usings = usingDeclarations.Select(CreateUsingFromUsingDeclarationSyntax);
      var content = root.GetText().ToString();
      return new CodeFile(filePath, content, namespaces, usings);
   }

   private Task<CodeFile> ReadCodeFileFromStringAsync(string fileContent, string filePath, CancellationToken cancellationToken)
   {
      return Task.Factory.StartNew(() => ReadCodeFileFromString(filePath, fileContent), cancellationToken);
   }

   #endregion
}