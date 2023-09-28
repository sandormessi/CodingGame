using System.Text;

namespace Playground;

internal class ClashOfCode
{
   private static char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
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
   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }
   private sealed class CharacterPair
   {
      public CharacterPair(char character1, char character2)
      {
         Character1 = character1;
         Character2 = character2;
      }

      public char Character1 { get; }

      public char Character2 { get; }
   }
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
   private static IReadOnlyList<int> CountItemGroups<TItem>(IReadOnlyList<TItem> list, TItem targetItem) where TItem : notnull
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


}