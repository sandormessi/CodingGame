namespace CodingGames.Certificates.SecondTry;

internal class SecondTask
{
   #region Public Methods and Operators

   public void Execute()
   {
      int n = int.Parse(Console.ReadLine());
      for (int i = 0; i < n; i++)
      {
         string[] inputs = Console.ReadLine().Split(' ');
         int b = int.Parse(inputs[0]);
         int t = int.Parse(inputs[1]);

         int tInFahrenheit = ConvertFromCelsiusToFahrenheit(t);

         if (b < tInFahrenheit)
         {
            Console.WriteLine("Lower");
         }
         else if (b < tInFahrenheit)
         {
            Console.WriteLine("Higher");
         }
         else
         {
            Console.WriteLine("Same");
         }
      }
   }

   #endregion

   #region Methods

   private static int ConvertFromCelsiusToFahrenheit(int celsius)
   {
      return (int)(celsius * (9d / 5) + 32);
   }

   #endregion
}