namespace CodingGames.Puzzles.Easy;

using System;
using System.Collections.Generic;

public class BankRobbers
{
   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   public static void Main()
   {
      int R = int.Parse(ReadInput());
      int V = int.Parse(ReadInput());

      List<long> combinations = new(V);
      for (int i = 0; i < V; i++)
      {
         string[] inputs = ReadInput().Split(' ');

         int C = int.Parse(inputs[0]);
         int N = int.Parse(inputs[1]);

         var firstPartCount = N;
         var secondPartCount = C - N;

         var allCombinations = (long)(Math.Pow(10, firstPartCount) + Math.Pow(5, secondPartCount));
         combinations.Add(allCombinations);
      }

      for (int j = 0; j < V; j += R)
      {
         
      }

      Console.WriteLine("1");
   }
}