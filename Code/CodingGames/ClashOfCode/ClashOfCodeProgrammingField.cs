namespace CodingGames.ClashOfCode;

using System.Text;

public class ClashOfCodeProgrammingField
{
   #region Constants and Fields

   private static char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

   #endregion

   #region Enums

   private enum Operation
   {
      Add,

      Sub,

      Multi,

      Divide
   }

   #endregion

   #region Methods

   private static IReadOnlyList<string> ChangeCharacters(IEnumerable<string> strings, IReadOnlyList<CharacterPair> characterPairs)
   {
      List<string> texts = new(16);

      foreach (string s in strings)
      {
         StringBuilder sb = new(s);

         for (var i = 0; i < sb.Length; i++)
         {
            char actualCharacter = sb[i];
            CharacterPair? found = characterPairs.FirstOrDefault(x => x.Character1 == actualCharacter);
            if (found is not null)
            {
               sb[i] = found.Character2;
            }
            else
            {
               CharacterPair? found2 = characterPairs.FirstOrDefault(x => x.Character2 == actualCharacter);
               if (found2 is not null)
               {
                  sb[i] = found2.Character1;
               }
            }
         }

         texts.Add(sb.ToString());
      }

      return texts;
   }

   private static IReadOnlyList<int> CountItemGroups<TItem>(IReadOnlyList<TItem> list, TItem targetItem)
      where TItem : notnull
   {
      List<int> counts = new(8);
      for (var i = 0; i < list.Count; i++)
      {
         var count = 0;
         while ((i < list.Count) && list[i].Equals(targetItem))
         {
            count++;
            i++;
         }

         if (count > 0)
         {
            counts.Add(count);
         }
      }

      return counts;
   }

   private static IReadOnlyList<long> DivisorsOf(long n)
   {
      List<long> divisors = new();
      for (long i = 1; i <= n; i++)
      {
         if (n % i == 0)
         {
            divisors.Add(i);
         }
      }

      return divisors;
   }

   private static long Factorial(int i)
   {
      if (i <= 1)
         return 1;
      return i * Factorial(i - 1);
   }

   private static long FactorialDivision(int topFactorial, int divisorFactorial)
   {
      long result = 1;
      for (int i = topFactorial; i > divisorFactorial; i--)
      {
         result *= i;
      }

      return result;
   }

   private static IEnumerable<int> GetDigits(long N)
   {
      return N.ToString().Take(4).Select(x => int.Parse(x.ToString()));
   }

   private static int GetNumber(IEnumerable<int> digits)
   {
      return int.Parse(digits.Select(x => x.ToString()).Aggregate((x1, x2) => x1 + x2));
   }

   private static bool IsPrime(int n)
   {
      return DivisorsOf(n).Count == 2;
   }

   private static long nCr(int n, int r)
   {
      return nPr(n, r) / Factorial(r);
   }

   private static long nPr(int n, int r)
   {
      return FactorialDivision(n, n - r);
   }

   private static string PrintValues<T>(IEnumerable<T> values, string separator)
   {
      return values.Select(x => x.ToString()).Aggregate((x1, x2) => $"{x1}{separator}{x2}");
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   private static char SwapCase(char c)
   {
      if (char.IsUpper(c))
      {
         return char.ToLower(c);
      }

      return char.ToUpper(c);
   }

   private static void SwapCaseInStringBuilder(StringBuilder stringBuilder)
   {
      for (var i = 0; i < stringBuilder.Length; i++)
      {
         stringBuilder[i] = SwapCase(stringBuilder[i]);
      }
   }

   #endregion

   private sealed class CharacterPair
   {
      #region Constructors and Destructors

      public CharacterPair(char character1, char character2)
      {
         Character1 = character1;
         Character2 = character2;
      }

      #endregion

      #region Public Properties

      public char Character1 { get; }

      public char Character2 { get; }

      #endregion
   }
}