namespace CodingGames.Puzzles.Hard;

public class TheHighestBuilding
{
   #region Methods

   private static void DebugMessage<T>(T message)
   {
      Console.Error.WriteLine(message);
   }

   private static int FindBuildingTopIndex(IReadOnlyList<char[]> city, Range lowerRange, char buildingCharacter)
   {
      for (var i = 0; i < city.Count; i++)
      {
         for (var j = lowerRange.Start.Value; j < lowerRange.End.Value; j++)
         {
            if (city[i][j] == buildingCharacter)
            {
               return i;
            }
         }
      }

      throw new InvalidOperationException("Highest building could not be found.");
   }

   static void Main()
   {
      const char buildingCharacter = '#';

      var inputs = ReadInput().Split(' ');

      var width = int.Parse(inputs[0]);
      var height = int.Parse(inputs[1]);

      var city = new char[height][];

      for (var i = 0; i < height; i++)
      {
         var row = ReadInput();
         DebugMessage(row);
         city[i] = row.ToCharArray();
      }

      var firstRow = city[^1];

      List<Building> buildings = new(width);

      for (var i = 0; i < firstRow.Length; i++)
      {
         if (firstRow[i] == buildingCharacter)
         {
            var start = i;
            while (i < firstRow.Length && firstRow[i] == buildingCharacter)
            {
               i++;
            }

            Range lowerRange = new(start, i);
            var highestBuildingRowIndex = FindBuildingTopIndex(city, lowerRange, buildingCharacter);
            var buildingHeight = height - highestBuildingRowIndex;
            var building = new Building(highestBuildingRowIndex, buildingHeight, lowerRange);

            DebugMessage(building.ToString());
            buildings.Add(building);
         }
      }

      var buildingOrdered = buildings.OrderByDescending(x => x.Height).ToArray();
      var highestBuilding = buildingOrdered[0].Height;

      var highestBuildings = buildingOrdered.Where(x => x.Height == highestBuilding).ToArray();
      for (var index = 0; index < highestBuildings.Length; index++)
      {
         var building = highestBuildings[index];

         WriteBuilding(city, building);

         if (index + 1 >= highestBuildings.Length)
         {
            break;
         }

         Console.WriteLine();
      }
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   private static void WriteBuilding(IReadOnlyList<char[]> city, Building building)
   {
      for (var i = building.HighestBuildingRowIndex; i < city.Count; i++)
      {
         var row = city[i][building.LowerRange];
         var rowString = new string(row).TrimEnd(' ');

         WriteOutput(rowString);
      }
   }

   private static void WriteOutput<T>(T answer)
   {
      Console.WriteLine(answer);
   }

   #endregion

   private class Building
   {
      #region Constructors and Destructors

      public Building(int highestBuildingRowIndex, int height, Range lowerRange)
      {
         HighestBuildingRowIndex = highestBuildingRowIndex;
         Height = height;
         LowerRange = lowerRange;
      }

      #endregion

      #region Public Properties

      public int Height { get; }

      public int HighestBuildingRowIndex { get; }

      public Range LowerRange { get; }

      #endregion

      #region Public Methods and Operators

      public override string ToString()
      {
         return $"Index: {HighestBuildingRowIndex}, Height: {Height}, Range: {LowerRange}";
      }

      #endregion
   }
}