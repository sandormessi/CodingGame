namespace CodingGames.Optimization;

using System;
using System.Collections.Generic;
using System.Linq;

public class Wordle
{
   #region Constants and Fields

   private const int WordLength = 6;

   #endregion

   #region Public Methods and Operators

   public static void Main()
   {
      var readLine = ReadInput();

      int wordCount = int.Parse(readLine);

      Console.Error.WriteLine(wordCount);

      var line = ReadInput();

      string[] inputs = line.Split(' ');

      List<string> wordSet = new();
      for (int i = 0; i < wordCount; i++)
      {
         string word = inputs[i];
         wordSet.Add(word.ToUpper());
      }

      List<string> possibleWords = wordSet.ToList();
      string wordGuess = possibleWords.First();

      List<string> guessedWordsSoFar = new();

      List<(char character, List<int> positionsChecked)> containAnyPosition = new();
      List<(int position, char character)> containExactPosition = new();
      List<char> notContains = new();

      int[] states = new int[WordLength];
      while (true)
      {
         var readInput = ReadInput();
         inputs = readInput.Split(' ');

         for (var i = 0; i < states.Length; i++)
         {
            int state = int.Parse(inputs[i]);
            states[i] = state;
         }

         var firstGuess = states.Any(x => x == 0);

         possibleWords = ProcessStates(firstGuess, states, wordGuess, possibleWords, guessedWordsSoFar, containAnyPosition, containExactPosition,
            notContains);

         if (!firstGuess)
         {
            WriteWords(possibleWords);
         }

         wordGuess = SelectWordToGuess(possibleWords);

         WriteWord(wordGuess);

         guessedWordsSoFar.Add(wordGuess);
      }
   }

   #endregion

   #region Methods

   private static bool IsWordContainsCharacters(string word, IEnumerable<(char character, List<int> positionsChecked)> characters)
   {
      foreach (var (character, positionsChecked) in characters)
      {
         if (!word.Contains(character))
         {
            return false;
         }

         for (var index = 0; index < word.Length; index++)
         {
            var letter = word[index];
            if (letter == character)
            {
               if (positionsChecked.Contains(index))
               {
                  return false;
               }
            }
         }
      }

      return true;
   }

   private static bool IsWordContainsCharactersInExactPosition(string word, List<(int position, char character)> charactersAtExactPosition)
   {
      foreach (var (position, character) in charactersAtExactPosition)
      {
         if (word[position] != character)
         {
            return false;
         }
      }

      return true;
   }

   private static bool IsWordNotContainsCharacters(string word, IEnumerable<char> characters)
   {
      return !characters.Any(word.Contains);
   }

   private static List<string> ProcessStates(bool firstGuess, IReadOnlyList<int> states, string wordGuess, List<string> possibleWords,
      IEnumerable<string> guessedWordsSoFar, List<(char character, List<int> positionsChecked)> containAnyPosition,
      List<(int position, char character)> containExactPosition, ICollection<char> notContains)
   {
      if (firstGuess)
      {
         return possibleWords;
      }

      for (var i = 0; i < states.Count; i++)
      {
         var state = states[i];
         var currentLetter = wordGuess[i];
         switch (state)
         {
            case 0:
               // Initial value
               // We should not get here
               break;
            case 1:

               notContains.Add(currentLetter);

               break;
            case 2:

               var foundIndex = containAnyPosition.FindIndex(x => x.character == currentLetter);
               if (foundIndex < 0)
               {
                  containAnyPosition.Add((currentLetter, new List<int> { i }));
               }
               else
               {
                  containAnyPosition[foundIndex].positionsChecked.Add(i);
               }

               break;

            case 3:

               containExactPosition.Add((i, currentLetter));

               break;
            default:
               throw new InvalidOperationException("Invalid input.");
         }
      }

      var possibleWords2 = possibleWords.Except(guessedWordsSoFar, StringComparer.CurrentCultureIgnoreCase)
         .Where(x => IsWordNotContainsCharacters(x, notContains)).Where(x => IsWordContainsCharacters(x, containAnyPosition))
         .Where(x => IsWordContainsCharactersInExactPosition(x, containExactPosition)).ToList();

      return possibleWords2;
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   private static string SelectWordToGuess(IReadOnlyList<string> possibleWords)
   {
      return possibleWords.OrderByDescending(x => x.Distinct().Count()).ThenBy(x => x, StringComparer.CurrentCultureIgnoreCase).First();
   }

   private static void WriteWord(string wordGuess)
   {
      Console.WriteLine(wordGuess.ToUpper());
   }

   private static void WriteWords(IEnumerable<string> words)
   {
      foreach (var word in words)
      {
         Console.Error.WriteLine(word);
      }
   }

   #endregion
}