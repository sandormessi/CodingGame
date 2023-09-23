namespace SudokuValidator;

public class SudokuValidatorGame
{
  public static void Main()
   {
      List<int[]> grid = new();
      for (var i = 0; i < 9; i++)
      {
         string[] inputs = ReadInput().Split(' ');
         var line = new int[9];
         for (var j = 0; j < 9; j++)
         {
            int n = int.Parse(inputs[j]);
            line[j] = n;
         }

         grid.Add(line);
      }

      for (var i = 0; i <= 6; i += 3)
      {
         for (var j = 0; j <= 6; j += 3)
         {
            if (!ValidateGridUniqueness(j, i, j + 2, i + 2, grid))
            {
               return;
            }

            if (!ValidateSubGrid(j, i, j + 2, i + 2, grid))
            {
               return;
            }
         }
      }

      if (!ValidateGridUniqueness(0, 0, 8, 8, grid))
      {
         return;
      }

      WriteAnswer(true);
   }

   private static string? ReadInput()
   {
      return Console.ReadLine();
   }

   private static bool ValidateGridUniqueness(int startX, int startY, int endX,
      int endY, IReadOnlyList<int[]> grid)
   {
      for (int i = startX; i <= endX; i++)
      {
         List<int> column = new(9);
         for (int j = startY; j <= endY; j++)
         {
            column.Add(grid[j][i]);
         }

         if (!ValidateLine(column.ToArray()))
         {
            WriteAnswer(false);
            return false;
         }
      }

      for (int i = startY; i <= endY; i++)
      {
         int[] line = grid[i][new Range(startX, endX + 1)];
         if (!ValidateLine(line))
         {
            WriteAnswer(false);
            return false;
         }
      }

      return true;
   }

   private static bool ValidateLine(IReadOnlyCollection<int> line)
   {
      return line.Distinct().Count() == line.Count;
   }

   private static bool ValidateSubGrid(int startX, int startY, int endX,
      int endY, IReadOnlyList<int[]> grid)
   {
      List<int> subGridNumbers = new(9);
      for (int i = startY; i <= endY; i++)
      {
         for (int j = startX; j <= endX; j++)
         {
            subGridNumbers.Add(grid[j][i]);
         }
      }

      if (subGridNumbers.Distinct().Count() != 9)
      {
         WriteAnswer(false);
         return false;
      }

      return true;
   }

   private static void WriteAnswer(bool isValid)
   {
      Console.WriteLine(isValid.ToString().ToLower());
   }
}
