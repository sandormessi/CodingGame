namespace CodingGames.Puzzles.Hard;

using System;
using System.Collections.Generic;
using System.Linq;

public class SimplifySelectionRanges
{
   #region Public Methods and Operators

   public static void Main()
   {
      var N = Console.ReadLine();

      var numbers = N[1..^1].Split(',').Select(int.Parse).OrderBy(x => x).ToArray();

      List<string> targetNumbers = new(numbers.Length);
      for (var i = 0; i < numbers.Length; i++)
      {
         var index = i;
         while (index + 1 < numbers.Length && numbers[index + 1] - numbers[index] == 1)
         {
            index++;
         }

         if (index == i || index - i + 1 < 3)
         {
            targetNumbers.Add(numbers[i].ToString());
         }
         else
         {
            targetNumbers.Add($"{numbers[i]}-{numbers[index]}");
            i = index;
         }
      }

      Console.WriteLine(PrintValues(targetNumbers, ","));
   }

   #endregion

   #region Methods

   private static string PrintValues<T>(IEnumerable<T> values, string separator)
   {
      return values.Select(x => x.ToString()).Aggregate((x1, x2) => $"{x1}{separator}{x2}");
   }

   #endregion
}