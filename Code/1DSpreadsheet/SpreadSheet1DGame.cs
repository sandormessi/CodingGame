namespace _1DSpreadsheet;

public class SpreadSheet1DGame
{
   #region Constants and Fields

   private const string AddOperation = "ADD";

   private const string MultiplicationOperation = "MULT";

   private const string SubtractOperation = "SUB";

   private const string ValueOperation = "VALUE";

   private static readonly Queue<string> DeepBirecursionTestCase = new(new List<string>()
   {
      "92",
      "SUB $33 $64",
      "ADD $60 $60",
      "ADD $61 $61",
      "SUB $76 $80",
      "SUB $25 $59",
      "ADD $58 $28",
      "ADD $88 $59",
      "ADD $32 $32",
      "ADD $83 $21",
      "ADD $69 $39",
      "ADD $57 $64",
      "ADD $26 $26",
      "ADD $1 $1",
      "SUB $62 $68",
      "ADD $73 $1",
      "ADD $50 $27",
      "SUB $24 $2",
      "ADD $14 $12",
      "ADD $10 $89",
      "SUB $67 $35",
      "ADD $58 $58",
      "ADD $7 $7",
      "SUB $0 $89",
      "ADD $20 $20",
      "SUB $43 $61",
      "SUB $53 $11",
      "ADD $37 $37",
      "ADD $82 $47",
      "ADD $90 $2",
      "ADD $89 $89",
      "ADD $85 $85",
      "SUB $91 $47",
      "ADD $69 $69",
      "SUB $46 $86",
      "SUB $42 $20",
      "ADD $12 $12",
      "ADD $56 $8",
      "ADD $72 $72",
      "ADD $9 $32",
      "ADD $30 $77",
      "ADD $80 $48",
      "ADD $79 $81",
      "SUB $16 $58",
      "SUB $44 $56",
      "SUB $63 $21",
      "ADD $20 $5",
      "SUB $49 $81",
      "ADD $54 $54",
      "ADD $29 $18",
      "SUB $34 $23",
      "ADD $47 $47",
      "SUB $74 $32",
      "SUB $17 $72",
      "SUB $71 $26",
      "ADD $59 $59",
      "ADD $15 $68",
      "ADD $21 $21",
      "ADD $86 $41",
      "ADD $2 $2",
      "ADD $11 $11",
      "ADD $80 $80",
      "ADD $56 $56",
      "SUB $31 $50",
      "SUB $51 $7",
      "ADD $86 $86",
      "ADD $72 $35",
      "SUB $75 $30",
      "SUB $70 $12",
      "ADD $50 $50",
      "ADD $30 $30",
      "SUB $84 $1",
      "SUB $52 $37",
      "VALUE 1 _",
      "ADD $40 $60",
      "SUB $66 $69",
      "SUB $13 $85",
      "SUB $22 $29",
      "ADD $55 $85",
      "ADD $37 $65",
      "ADD $23 $45",
      "ADD $29 $29",
      "ADD $23 $23",
      "ADD $54 $6",
      "ADD $38 $7",
      "SUB $3 $60",
      "ADD $68 $68",
      "ADD $81 $81",
      "ADD $78 $26",
      "ADD $87 $11",
      "ADD $64 $64",
      "ADD $61 $36",
      "SUB $4 $54"
   });

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