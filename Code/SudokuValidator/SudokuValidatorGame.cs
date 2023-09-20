namespace SudokuValidator;

public class SudokuValidatorGame
{
   static Queue<string> queue = new Queue<string>(new List<string>
   {
      "1 2 3 4 5 6 7 8 9",
      "4 5 6 7 8 9 1 2 3",
      "7 8 9 1 2 3 4 5 6",
      "9 1 2 3 4 5 6 7 8",
      "3 4 5 6 7 8 9 1 2",
      "6 7 8 9 1 2 3 4 5",
      "8 9 1 2 3 4 5 6 7",
      "2 3 4 5 6 7 8 9 1",
      "5 6 7 8 9 1 2 3 4"
   });
   private static string? ReadInput()
   {
      return Console.ReadLine();
   }
   public static void Main()
   {
      List<int[]> grid = new ();
      for (int i = 0; i < 9; i++)
      {
         string[] inputs = ReadInput().Split(' ');
         int[] line = new int[9];
         for (int j = 0; j < 9; j++)
         {
            int n = int.Parse(inputs[j]);
            line[j] = n;
         }

         grid.Add(line);
      }

      if (!ValidateGrid(0,0,2,2,grid))
         return;
      
      if (!ValidateGrid(3,0,5,2,grid))
         return;
      
      if (!ValidateGrid(6,0,8,2,grid))
         return;

      if (!ValidateGrid(0,3,2,5,grid))
         return;
      
      if (!ValidateGrid(3,3,5,5,grid))
         return;
      
      if (!ValidateGrid(6,3,8,5,grid))
         return;
      
      if (!ValidateGrid(0,6,2,8,grid))
         return;
      
      if (!ValidateGrid(3,6,5,8,grid))
         return;
      
      if (!ValidateGrid(6,6,8,8,grid))
         return;

      if (!ValidateGrid(0, 0, 8, 8, grid))
         return;

      WriteAnswer(true);
   }

  
   private static bool ValidateGrid(int startX,int startY,int endX,int endY,List<int[]> grid)
   {
      for (int i = startX; i <= endX; i++)
      {
         List<int> column = new(9);
         for (int j = startY; j <= endY; j++)
         {
            column.Add(grid[j][i]);
         }

         if (!ValidateLine(column.ToArray(), startY, endY))
         {
            WriteAnswer(false);
            return false;
         }
      }

      for (var i = startY; i <= endY; i++)
      {
         var line = grid[i];
         if (!ValidateLine(line, startX, endX))
         {
            WriteAnswer(false);
            return false;
         }
      }

      return true;
   }

   private static bool ValidateLine(int[] line, int startX, int endX)
   {
      var count = endX - startX + 1;
      if (count == line.Length)
      {
         return line.Distinct().Count() == count;
      }

      var range = line[new Range(startX, endX + 1)];
      return range.Distinct().Count() == count;
   }

   private static void WriteAnswer(bool isValid)
   {
      Console.WriteLine(isValid.ToString().ToLower());
   }
}