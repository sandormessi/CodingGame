namespace CodingGames.Puzzles.Easy;

using System;
using System.Collections.Generic;
using System.Linq;

public class HorseRacingDuals
{
   #region Methods

   static void Main(string[] args)
   {
      int N = int.Parse(Console.ReadLine());
      List<int> powers = new(N);
      for (int i = 0; i < N; i++)
      {
         int pi = int.Parse(Console.ReadLine());
         powers.Add(pi);
      }

      long difference = long.MaxValue;
      var ordered = powers.OrderBy(x => x).ToArray();

      for (int i = 1; i < ordered.Length; i++)
      {
         var previous = ordered[i - 1];
         var actual = ordered[i];

         var difference2 = Math.Abs(previous - actual);
         if (difference2 < difference)
         {
            difference = difference2;
         }
      }

      Console.WriteLine(difference);
   }

   #endregion
}