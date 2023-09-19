namespace NetworkCabling;

using System.Drawing;
using System.Linq;

public class NetworkCablingGame
{
   private static List<Point> GetPoints()
   {
      var input =
         "-28189131 593661218;102460950 1038903636;938059973 -816049599;-334087877 -290840615;842560881 -116496866;-416604701 690825290;19715507 470868309;846505116 -694479954";

      var coords = input.Split(';').Select(x => x.Split(' '));
      return coords.Select(x => new Point(int.Parse(x[0]), int.Parse(x[1]))).ToList();
   }
   
   private static List<Point> GetPoints2()
   {
      var input = "0 0;1 1;2 2";

      var coords = input.Split(';').Select(x => x.Split(' '));
      return coords.Select(x => new Point(int.Parse(x[0]), int.Parse(x[1]))).ToList();
   }


   public static void Main()
   {
      int n = int.Parse(Console.ReadLine());

      List<Point> coordinates = new(n);
      for (var i = 0; i < n; i++)
      {
         string[] inputs = Console.ReadLine().Split(' ');
         coordinates.Add(new Point(int.Parse(inputs[0]), int.Parse(inputs[1])));
      }

      var cords = coordinates.OrderBy(x => x.Y).ToArray();

      int half = cords.Length / 2;

      double median = cords.Length % 2 == 0
         ? (cords[half].Y + cords[half + 1].Y) / 2d
         : cords[half].Y;

      var med = (int)Math.Round(median);

      var lower = cords.Where(x => x.Y < med);
      var equal = cords.Where(x => x.Y == med);
      var greater = cords.Where(x => x.Y > med);

      if (!equal.Any())
      {
         var nb = lower.Max(x => x.Y);
         var na = greater.Min(x => x.Y);

         med = Math.Min(na - med, med - nb);
      }

      long cableLength = cords.Aggregate<Point, long>(0, (current, actual) => current + Math.Abs(actual.Y - med));

      cableLength += coordinates.Max(p => p.X) - coordinates.Min(p => p.X);

      Console.WriteLine(cableLength);
   }

   public static void Main2()
   {
      int n = int.Parse(Console.ReadLine());

      List<Point> coordinates = new(n);
      for (var i = 0; i < n; i++)
      {
         string[] inputs = Console.ReadLine().Split(' ');
         coordinates.Add(new Point(int.Parse(inputs[0]), int.Parse(inputs[1])));
      }

      var cords = coordinates.OrderBy(x => x.Y).ToArray();

      int half = cords.Length / 2;

      double median = cords.Length % 2 == 0
         ? (cords[half].Y + cords[half + 1].Y) / 2d
         : cords[half].Y;

      var med = (int)Math.Round(median);
      
      var closest = cords.OrderBy(x => Math.Abs(x.Y - med)).ToArray();

      // TODO chose from these to be the median of points below and above the median
      var closest1 = closest[0];
      var closest2 = closest[1];

      var lower = cords.Where(x => x.Y < med);
      var equal = cords.Where(x => x.Y == med);
      var greater = cords.Where(x => x.Y > med);

      if (!equal.Any())
      {
         var nb = lower.Max(x => x.Y);
         var na = greater.Min(x => x.Y);

         med = Math.Min(na - med, med - nb);
      }

      long cableLength = cords.Aggregate<Point, long>(0, (current, actual) => current + Math.Abs(actual.Y - med));

      cableLength += coordinates.Max(p => p.X) - coordinates.Min(p => p.X);

      Console.WriteLine(cableLength);
   }
}