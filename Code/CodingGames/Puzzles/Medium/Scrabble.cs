namespace CodingGames.Puzzles.Medium;

public class Scrabble
{
   #region Constants and Fields

   private static readonly IReadOnlyList<WordValue> wordValues = new[]
   {
      new WordValue(new[] { 'a', 'e', 'i', 'o', 'n', 'r', 't', 'l', 's', 'u' }, 1), new WordValue(new[] { 'd', 'g' }, 2),
      new WordValue(new[] { 'b', 'c', 'm', 'p' }, 3), new WordValue(new[] { 'f', 'h', 'v', 'w', 'y' }, 4), new WordValue(new[] { 'k' }, 5),
      new WordValue(new[] { 'j', 'x' }, 8), new WordValue(new[] { 'q', 'z' }, 10)
   };

   #endregion

   #region Public Methods and Operators

   public static void Main()
   {
      int N = int.Parse(Console.ReadLine());

      string[] words = new string[N];
      for (int i = 0; i < N; i++)
      {
         string W = Console.ReadLine();
         words[i] = W;
      }

      char[] letters = Console.ReadLine().OrderBy(x => x).ToArray();

      Console.Error.WriteLine(new string(letters));
      Console.Error.WriteLine("------------");

      var potentialWords = words.Where(word => letters.Intersect(word).Any()).OrderByDescending(x => letters.Intersect(x).Count());

      List<Word> wordList = new(16);

      foreach (var word in words)
      {
         int value = 0;
         var commonCharacters = word.Intersect(letters).OrderBy(x => x).ToArray();
         foreach (var character in commonCharacters)
         {
            if (word.Count(x => x == character) > letters.Count(x => x == character) || word.Any(x => !letters.Contains(x)))
            {
               value = 0;
               break;
            }

            var foundWordValue = wordValues.FirstOrDefault(x => x.Characters.Contains(character));
            if (foundWordValue != null)
            {
               value += foundWordValue.Value;
            }
         }

         Console.Error.WriteLine($"{word},{value}");
         wordList.Add(new Word(word, commonCharacters, value));
      }

      Console.WriteLine(wordList.MaxBy(x => x.Value).TheWord);
   }

   #endregion

   private sealed class Word
   {
      #region Constructors and Destructors

      public Word(string theWord, IReadOnlyList<char> commonValues, int value)
      {
         TheWord = theWord;
         CommonValues = commonValues;
         Value = value;
      }

      #endregion

      #region Public Properties

      public IReadOnlyList<char> CommonValues { get; }

      public string TheWord { get; }

      public int Value { get; }

      #endregion
   }

   private sealed class WordValue
   {
      #region Constructors and Destructors

      public WordValue(IReadOnlyList<char> characters, int value)
      {
         Characters = characters ?? throw new ArgumentNullException(nameof(characters));
         Value = value;
      }

      #endregion

      #region Public Properties

      public IReadOnlyList<char> Characters { get; }

      public int Value { get; }

      #endregion
   }
}