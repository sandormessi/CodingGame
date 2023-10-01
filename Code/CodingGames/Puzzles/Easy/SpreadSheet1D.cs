namespace CodingGames.Puzzles.Easy;

public class SpreadSheet1D
{
   #region Constants and Fields

   private const string AddOperation = "ADD";

   private const string MultiplicationOperation = "MULT";

   private const string SubtractOperation = "SUB";

   private const string ValueOperation = "VALUE";

   #endregion

   #region Public Methods and Operators

   public static void Main()
   {
      int cellCount = ConvertStringToInt32(ReadInput());

      List<Cell> cells = new(cellCount);
      for (int i = 0; i < cellCount; i++)
      {
         string[] inputs = ReadInput().Split(' ');
         string operation = inputs[0];
         string arg1 = inputs[1];
         string arg2 = inputs[2];

         var cell = new Cell(operation, arg1, arg2);
         cells.Add(cell);
      }

      long[] finalCells = Enumerable.Repeat(long.MinValue, cellCount).ToArray();

      for (int j = 0; j < cellCount; j++)
      {
         finalCells[j] = GetCellValue(finalCells, cells, j);
         Console.WriteLine(finalCells[j]);
      }
   }

   #endregion

   #region Methods

   private static int ConvertStringToInt32(string numberString)
   {
      return int.Parse(numberString);
   }

   private static int GetCellValue(IList<long> finalCells, IReadOnlyList<Cell> cells, int cellIndex)
   {
      var finalCell = finalCells[cellIndex];
      if (finalCell != long.MinValue)
      {
         return (int)finalCell;
      }

      var cell = cells[cellIndex];

      var parameter1 = cell.Arg1;
      var parameter2 = cell.Arg2;

      if (cell.Operation.Equals(ValueOperation, StringComparison.CurrentCultureIgnoreCase))
      {
         var value = GetValueOfParameter(finalCells, cells, parameter1);

         finalCells[cellIndex] = value;
         return value;
      }

      int arg1 = GetValueOfParameter(finalCells, cells, parameter1);
      int arg2 = GetValueOfParameter(finalCells, cells, parameter2);

      if (cell.Operation.Equals(AddOperation, StringComparison.CurrentCultureIgnoreCase))
      {
         var cellValue = arg1 + arg2;
         finalCells[cellIndex] = cellValue;
         return cellValue;
      }

      if (cell.Operation.Equals(SubtractOperation, StringComparison.CurrentCultureIgnoreCase))
      {
         var cellValue = arg1 - arg2;
         finalCells[cellIndex] = cellValue;
         return arg1 - arg2;
      }

      if (cell.Operation.Equals(MultiplicationOperation, StringComparison.CurrentCultureIgnoreCase))
      {
         var cellValue = arg1 * arg2;
         finalCells[cellIndex] = cellValue;
         return arg1 * arg2;
      }

      throw new InvalidOperationException("Invalid operation.");
   }

   private static int GetValueOfParameter(IList<long> finalCells, IReadOnlyList<Cell> cells, string parameter)
   {
      if (parameter.StartsWith("$"))
      {
         var cellIndexOfArg1 = ConvertStringToInt32(parameter.Remove(0, 1));
         return GetCellValue(finalCells, cells, cellIndexOfArg1);
      }

      return ConvertStringToInt32(parameter);
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   #endregion

   private class Cell
   {
      #region Constructors and Destructors

      public Cell(string operation, string arg1, string arg2)
      {
         Operation = operation;
         Arg1 = arg1;
         Arg2 = arg2;
      }

      #endregion

      #region Public Properties

      public string Arg1 { get; }

      public string Arg2 { get; }

      public string Operation { get; }

      #endregion
   }
}