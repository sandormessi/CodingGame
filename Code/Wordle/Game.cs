namespace Wordle;

internal class Game
{
   #region Constants and Fields

   private const int WordLength = 6;

   #endregion

   #region Public Methods and Operators

   public void Execute()
   {
      var readLine = ReadInput();

      int wordCount = int.Parse(readLine); // Number of words in the word set
      var line = ReadInput();

      string[] inputs = line.Split(' ');

      List<string> wordSet = new List<string>();
      for (int i = 0; i < wordCount; i++)
      {
         string word = inputs[i]; // Word in the word set
         wordSet.Add(word);
      }

      IList<string> possibleWords = wordSet.ToList();
      string wordGuess = possibleWords.First();

      IList<string> guessedWordsSoFar = new List<string>();

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

         possibleWords = ProcessStates(states, wordGuess, wordSet, possibleWords, guessedWordsSoFar);

         wordGuess = SelectWordToGuess(possibleWords);

         WriteWord(wordGuess);

         guessedWordsSoFar.Add(wordGuess);
      }
   }

   #endregion

   #region Methods

   private static bool IsWordContainsCharacters(string word, IEnumerable<char> characters)
   {
      return characters.All(word.Contains);
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

   private static IList<string> ProcessStates(IReadOnlyList<int> states, string wordGuess, IEnumerable<string> wordSet, IList<string> possibleWords,
      IEnumerable<string> guessedWordsSoFar)
   {
      if (states.Any(x => x == 0))
      {
         return possibleWords;
      }

      List<char> containAnyPosition = new();
      List<(int position, char character)> containExactPosition = new();
      List<char> notContains = new();

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

               containAnyPosition.Add(currentLetter);

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

   #endregion
}