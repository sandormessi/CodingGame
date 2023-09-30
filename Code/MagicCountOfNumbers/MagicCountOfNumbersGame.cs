namespace MagicCountOfNumbers;

public class MagicCountOfNumbersGame
{
   public static void Main()
   {
      string[] inputs = ReadInput().Split(' ');

      var maximumNumber = int.Parse(inputs[0]);
      var primeCount = int.Parse(inputs[1]);

      inputs = ReadInput().Split(' ');

      List<int> primes = new(primeCount);
      for (var i = 0; i < primeCount; i++)
      {
         var prime = int.Parse(inputs[i]);
         primes.Add(prime);
      }

      primes.Sort();
      long count = 0;

      for (int i = 1; i <= maximumNumber; i++)
      {
         foreach (var prime in primes)
         {
            if (i % prime == 0)
            {
               count++;
               break;
            }
         }
      }

      WriteAnswer(count);
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   private static void WriteAnswer(long answer)
   {
      Console.WriteLine(answer);
   }
}