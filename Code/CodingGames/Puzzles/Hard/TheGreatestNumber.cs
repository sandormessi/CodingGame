namespace CodingGames.Puzzles.Hard;

using System;
using System.Collections.Generic;
using System.Linq;

public class TheGreatestNumber
{
   #region Methods

   static void Main()
   {
      int N = int.Parse(ReadInput());
      string input = ReadInput();

      Console.Error.WriteLine(input);

      var digitStrings = input.Split(' ');

      bool isNegative = digitStrings.Any(x => x == "-");
      bool isWholeNumber = digitStrings.All(x => x != ".");

      var digitNumbers = digitStrings.Where(x => char.IsDigit(x[0])).Select(int.Parse).ToArray();

      IEnumerable<int> finalDigits;
      if (isNegative)
      {
         var digits = digitNumbers.OrderBy(x => x).ToList();
         finalDigits = digits;
      }
      else
      {
         var digits = digitNumbers.OrderByDescending(x => x).ToList();
         finalDigits = digits;
      }

      var number = PrintValues(finalDigits, string.Empty);

      if (isNegative)
      {
         number = "-" + number;

         if (!isWholeNumber)
         {
            number = number.Insert(2, ".");
         }

         bool isInvalidNegativeNumber = number.Skip(1).All(x => x is '0' or '.');
         if (isInvalidNegativeNumber)
         {
            WriteNumber("0");
            return;
         }
      }
      else
      {
         if (!isWholeNumber)
         {
            number = number.Insert(number.Length - 1, ".");
         }
      }

      WriteNumber(number.TrimEnd('0').Trim('.'));
   }

   private static string PrintValues<T>(IEnumerable<T> values, string separator)
   {
      return values.Select(x => x.ToString()).Aggregate((x1, x2) => $"{x1}{separator}{x2}");
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   private static void WriteNumber(string number)
   {
      Console.WriteLine(number);
   }

   #endregion
}