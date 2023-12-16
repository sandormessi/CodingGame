namespace CodingGames.Puzzles.Medium;

using System;
using System.Collections.Generic;
using System.Linq;

public class StockExchangeLosses
{
   #region Public Methods and Operators

   public static void Main()
   {
      int n = int.Parse(ReadInput());
      string[] inputs = ReadInput().Split(' ');

      int[] values = new int[n];
      for (int i = 0; i < n; i++)
      {
         int v = int.Parse(inputs[i]);
         values[i] = v;
      }

      List<int> differences = new(16);
      for (int i = 0; i < values.Length; i++)
      {
         var actualValue = values[i];

         int minValue = actualValue;
         for (int j = i + 1; j < values.Length; j++)
         {
            var actualCheckedValue = values[j];
            if (actualCheckedValue >= actualValue)
            {
               break;
            }

            if (actualCheckedValue < minValue)
            {
               minValue = actualCheckedValue;
            }
         }

         differences.Add(minValue - actualValue);
      }

      var value = differences.Min();

      Console.WriteLine(value);
   }

   #endregion

   #region Methods

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   #endregion
}