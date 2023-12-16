namespace CodingGames.Puzzles.Easy;

using System;

public class EuclidAlgorithm
{
   #region Public Methods and Operators

   public static void Main()
   {
      string[] inputs = ReadInput().Split(' ');

      int a = int.Parse(inputs[0]);
      int b = int.Parse(inputs[1]);

      var gcd = Euclid(a, b);

      Console.WriteLine($"GCD({a},{b})={gcd}");
   }

   #endregion

   #region Methods

   private static int Euclid(int a, int b)
   {
      var r = a % b;

      Console.WriteLine($"{a}={b}*{a / b}+{r}");

      if (r == 0)
      {
         return b;
      }

      return Euclid(b, r);
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   #endregion
}