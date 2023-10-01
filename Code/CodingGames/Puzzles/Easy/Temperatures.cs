namespace CodingGames.Puzzles.Easy;

public class Temperatures
{
   #region Methods

   static void Main(string[] args)
   {
      int n = int.Parse(Console.ReadLine());
      string[] inputs = Console.ReadLine().Split(' ');

      List<int> temperatures = new();
      for (int i = 0; i < n; i++)
      {
         temperatures.Add(int.Parse(inputs[i]));
      }

      long result = 0;
      if (n > 0)
      {
         var ordered = temperatures.OrderBy(x => Math.Abs(0 - x)).ThenByDescending(x => x);
         result = ordered.First();
      }

      Console.WriteLine(result);
   }

   #endregion
}