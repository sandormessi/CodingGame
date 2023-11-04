namespace CodingGames.Certificates.ThirdTry;

using System.Text;

public class ThirdTask
{
   public static void Main()
   {
      List<string> rows=new(9);
      int missingRowIndex = -1;
      for (int i = 0; i < 9; i++)
      {
         string row = Console.ReadLine();
         if (row.Contains("?"))
         {
            missingRowIndex=i;
         }
         rows.Add(row);
      }

      List<List<int>> columns = new(9);
      for (int i = 0; i < 9; i++)
      {
         List<int> column = new(8);
         columns.Add(column);
         for (int j = 0; j < 9; j++)
         {
            if (j == missingRowIndex)
            {
               continue;
            }

            var actualCell = rows[j][i];
            column.Add(int.Parse(actualCell.ToString()));
         }
      }

      var sum = Enumerable.Range(1, 9).Sum();
      for (int i = 0; i < columns.Count; i++)
      {
         var actualColumn = columns[i];
         var sumOfColumn = actualColumn.Sum();
         Console.Error.WriteLine(sumOfColumn);
         Console.Write(sum - sumOfColumn);
      }

      Console.WriteLine();
   }
}