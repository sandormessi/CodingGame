namespace NetworkCabling;

using System.Drawing;
using System.Linq;

internal class NetworkCablingGame
{
   public static void Main()
   {
      int n = int.Parse(Console.ReadLine());

      List<Point> coordinates = new(n);
      for (var i = 0; i < n; i++)
      {
         string[] inputs = Console.ReadLine().Split(' ');
         coordinates.Add(new Point(int.Parse(inputs[0]), int.Parse(inputs[1])));
      }

      Point[] orderedByYCoordinate = coordinates.OrderBy(x => x.Y).ToArray();

      int half = orderedByYCoordinate.Length / 2;

      double median = orderedByYCoordinate.Length % 2 == 0
         ? (orderedByYCoordinate[half].Y + orderedByYCoordinate[half + 1].Y) / 2d
         : orderedByYCoordinate[half].Y;

      var integerMedian = (int)Math.Round(median);

      Console.Error.WriteLine($"Median: {integerMedian}");

      long cableLength = CalculateCableLength(orderedByYCoordinate.Where(x => x.Y < integerMedian), integerMedian);
      cableLength += CalculateCableLength(orderedByYCoordinate.Where(x => x.Y >= integerMedian), integerMedian);

      int maxX = coordinates.Max(p => p.X);
      Console.Error.WriteLine("MaxX: " + maxX);

      int minX = coordinates.Min(p => p.X);
      Console.Error.WriteLine("MinX: " + minX);

      long mainCableLength = maxX - minX;

      Console.Error.WriteLine("Main cable length= " + mainCableLength);

      cableLength += mainCableLength;

      Console.WriteLine(cableLength);
   }

   private static long CalculateCableLength(IEnumerable<Point> points, int integerMedian)
   {
      long cableLength = 0;
      foreach (Point actual in points)
      {
         Point point1 = actual with { Y = integerMedian };
         var distanceFromMainCable = (long)Math.Sqrt(Math.Pow(point1.X - actual.X, 2) + Math.Pow(point1.Y - actual.Y, 2));

         Console.Error.WriteLine("Distance from main cable: " + distanceFromMainCable + $", Points: ({point1.X},{point1.Y}),({actual.X},{actual.Y})");

         cableLength += distanceFromMainCable;
      }

      return cableLength;
   }
}