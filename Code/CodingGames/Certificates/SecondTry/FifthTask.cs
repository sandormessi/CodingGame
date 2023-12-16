namespace CodingGames.Certificates.SecondTry;

using System;
using System.Linq;

internal class FifthTask
{
   #region Public Methods and Operators

   public void Execute()
   {
      int N = int.Parse(Console.ReadLine());
      for (int i = 0; i < N; i++)
      {
         string[] inputs = Console.ReadLine().Split(' ');
         int X = int.Parse(inputs[0]);
         int Y = int.Parse(inputs[1]);
         int Z = int.Parse(inputs[2]);

         int[] numbers = new int[] { X, Y, Z };
         var orderedNumbers = numbers.OrderBy(x => x).ToArray();
         Console.WriteLine(orderedNumbers[1]);
      }
   }

   #endregion
}