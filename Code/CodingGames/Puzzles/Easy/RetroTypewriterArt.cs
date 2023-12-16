namespace CodingGames.Puzzles.Easy;

using System;
using System.Linq;
using System.Text;

public class RetroTypewriterArt
{
   #region Public Methods and Operators

   public static void Main()
   {
      string T = ReadInput();

      var chunks = T.Split(' ');

      StringBuilder recipeStringBuilder = new(T.Length);

      foreach (var chunk in chunks)
      {
         var lastCharacter = chunk[^1];
         var beforeLastCharacter = chunk[^2];

         if (beforeLastCharacter == 's' && lastCharacter == 'p')
         {
            AddCharacters(recipeStringBuilder, chunk, ' ', 1);
         }
         else if (beforeLastCharacter == 'b' && lastCharacter == 'S')
         {
            AddCharacters(recipeStringBuilder, chunk, '\\', 1);
         }
         else if (beforeLastCharacter == 's' && lastCharacter == 'Q')
         {
            AddCharacters(recipeStringBuilder, chunk, '\'', 1);
         }
         else if (beforeLastCharacter == 'n' && lastCharacter == 'l')
         {
            AddCharacters(recipeStringBuilder, chunk, '\r', 1);
         }
         else
         {
            AddCharacters(recipeStringBuilder, chunk, lastCharacter, 0);
         }
      }

      Console.WriteLine(recipeStringBuilder.ToString());
   }

   #endregion

   #region Methods

   private static void AddCharacters(StringBuilder recipeStringBuilder, string chunk, char characterToRepeat, int addition)
   {
      var numberPart = chunk[..^(1 + addition)];
      Console.Error.WriteLine(numberPart);

      if (numberPart.Length == 0)
      {
         recipeStringBuilder.AppendLine();
         return;
      }

      var quantity = int.Parse(numberPart);

      var characters = Enumerable.Repeat(characterToRepeat, quantity);
      foreach (var character in characters)
      {
         recipeStringBuilder.Append(character);
      }
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   #endregion
}