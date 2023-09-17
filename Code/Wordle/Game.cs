namespace Wordle;

internal class Game
{
   #region Constants and Fields

   private const int WordLength = 6;

   #endregion

   #region Public Methods and Operators

   public static void Execute()
   {
      var readLine = ReadInput();

      int wordCount = int.Parse(readLine); // Number of words in the word set
      var line = ReadInput();

      string[] inputs = line.Split(' ');

      List<string> wordSet = new();
      for (int i = 0; i < wordCount; i++)
      {
         string word = inputs[i]; // Word in the word set
         wordSet.Add(word);
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
            int state = int.Parse(inputs[i]); // State of the letter of the corresponding position of previous guess
            states[i] = state;
         }

         possibleWords = ProcessStates(states, wordGuess, possibleWords, guessedWordsSoFar, containAnyPosition, containExactPosition, notContains);

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

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   private static string SelectWordToGuess(IEnumerable<string> possibleWords)
   {
      var wordGuess = possibleWords.First();

      return wordGuess;
   }

   private static void WriteWord(string wordGuess)
   {
      Console.WriteLine(wordGuess.ToUpper());
   }

   private static List<string> ProcessStates(IReadOnlyList<int> states, string wordGuess, List<string> possibleWords, IEnumerable<string> guessedWordsSoFar,
      List<(char character, List<int> positionsChecked)> containAnyPosition, List<(int position, char character)> containExactPosition,
      ICollection<char> notContains)
   {
      if (states.Any(x => x == 0))
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
                  containAnyPosition.Add((currentLetter, new() { i }));
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

      var possibleWords2 = possibleWords
         .Where(x => IsWordNotContainsCharacters(x, notContains))
         .Where(x => IsWordContainsCharacters(x, containAnyPosition))
         .Where(x => IsWordContainsCharactersInExactPosition(x, containExactPosition))
         .Except(guessedWordsSoFar, StringComparer.CurrentCultureIgnoreCase)
         .ToList();

      return possibleWords2;
   }

   #endregion
}