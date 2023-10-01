namespace CodingGames.Certificates.SecondTry;

internal class FourthTask
{
   #region Public Methods and Operators

   public void Execute()
   {
      int n = int.Parse(Console.ReadLine());
      var number = n;

      List<int> numbers = new List<int>() { number };

      while (number != 1)
      {
         if (number % 2 == 0)
         {
            number /= 2;
         }
         else
         {
            number = number * 3 + 1;
         }

         numbers.Add(number);
      }

      Console.WriteLine(numbers.Select(x => x.ToString()).Aggregate((x1, x2) => $"{x1} {x2}"));
   }

   #endregion
}