namespace CodingGames.Puzzles.Medium;

public class TheGift
{
   #region Public Methods and Operators

   public static void Main()
   {
      int n = int.Parse(ReadInput());

      int giftPrice = int.Parse(ReadInput());

      IList<int> budgets = new List<int>(n);
      for (int i = 0; i < n; i++)
      {
         int b = int.Parse(ReadInput());
         budgets.Add(b);
      }

      budgets = budgets.OrderBy(x => x).ToArray();

      var allBudget = budgets.Sum();
      if (allBudget < giftPrice)
      {
         Console.WriteLine("IMPOSSIBLE");
      }
      else
      {
         var targetDistributionPerPerson = giftPrice / budgets.Count;

         int actualBudget = 0;
         int remainingPrice = giftPrice;

         List<int> distributions = new(budgets.Count);
         for (var index = 0; index < budgets.Count; index++)
         {
            var remainingBudgetCount = budgets.Count - index;
            targetDistributionPerPerson = remainingPrice / remainingBudgetCount;

            var budget = budgets[index];
            if (budget <= targetDistributionPerPerson)
            {
               if (remainingPrice >= budget)
               {
                  distributions.Add(budget);
               }
               else
               {
                  distributions.Add(budget - remainingPrice);
               }
            }
            else
            {
               distributions.Add(targetDistributionPerPerson);
            }

            var distribution = distributions[^1];
            remainingPrice -= distribution;
         }

         foreach (var budget in distributions.OrderBy(x => x))
         {
            Console.WriteLine(budget);
         }
      }
   }

   private static string ReadInput()
   {
      var readInput = Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
      Console.Error.WriteLine(readInput);
      return readInput;
   }

   #endregion
}