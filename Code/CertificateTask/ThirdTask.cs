namespace CertificateTask;

using System.Text;

internal class ThirdTask
{
   #region Public Methods and Operators

   public void Execute()
   {
      int N = int.Parse(Console.ReadLine());
      string[] inputs = Console.ReadLine().Split(' ');

      List<int> digits = new List<int>();
      for (int i = 0; i < N; i++)
      {
         int digit = int.Parse(inputs[i]);
         digits.Add(digit);
      }

      var numbersInAscOrder = digits.OrderByDescending(x => x);
      StringBuilder numberStringBuilder = new();
      foreach (var i in numbersInAscOrder)
      {
         numberStringBuilder.Append(i);
      }

      var targetNumber = numberStringBuilder.ToString().TrimStart('0');
      if (targetNumber== string.Empty)
      {
         targetNumber = "0";
      }

      Console.WriteLine(targetNumber);
   }

   #endregion
}