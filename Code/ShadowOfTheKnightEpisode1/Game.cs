namespace ShadowOfTheKnightEpisode1;

public class Game
{
   private enum Directions
   {
      None,

      U,

      UR,

      R,

      DR,

      D,

      DL,

      L,

      UL
   }

   public static void Main()
   {
      var inputs = ReadInput().Split(' ');

      var buildingWidth = int.Parse(inputs[0]);
      var buildingHeight = int.Parse(inputs[1]);

      var maximumStep = int.Parse(ReadInput());

      inputs = ReadInput().Split(' ');

      var startX = int.Parse(inputs[0]);
      var startY = int.Parse(inputs[1]);

      List<Coordinate2d> positions = new();

      Coordinate2d actualPosition = new(startX, startY);

      while (true)
      {
         var bombDirection = ReadInput();

         var inputConvertedToEnum = (Directions)Enum.Parse(typeof(Directions), bombDirection, true);

         var nextPosition = CalculateTargetSquare(actualPosition, positions, inputConvertedToEnum, buildingWidth, buildingHeight);

         positions.Add(nextPosition);

         actualPosition = nextPosition;

         WriteCoordinates(nextPosition);
      }
   }

   private static void WriteCoordinates(Coordinate2d point)
   {
      Console.WriteLine($"{point.X} {point.Y}");
   }

   private sealed class Coordinate2d
   {
      public Coordinate2d(int x, int y)
      {
         X = x;
         Y = y;
      }

      public int X { get;  }
      public int Y { get;  }
   }

   private static Coordinate2d CalculateTargetSquare(Coordinate2d actualPosition,
      IReadOnlyList<Coordinate2d> positionsSoFar, Directions direction, int buildingWidth, int buildingHeight)
   {
      Coordinate2d corner;

      var targetX = -1;
      var targetY = -1;

      switch (direction)
      {
         case Directions.None:
            break;
         case Directions.U:

            corner = positionsSoFar
                        .Where(p => p.Y < actualPosition.Y)
                        .OrderByDescending(p => p.Y)
                        .FirstOrDefault() ?? new Coordinate2d(actualPosition.X, 0);

            targetX = actualPosition.X;
            targetY = actualPosition.Y - RoundNumber((actualPosition.Y - corner.Y) / 2d);

            break;
         case Directions.UR:

            if (!positionsSoFar.Any())
            {
               corner = new Coordinate2d(buildingWidth - 1, 0);
            }
            else
            {
               var cornerCandidate = positionsSoFar
                  .Where(p => p.X > actualPosition.X)
                  .Where(p => p.Y < actualPosition.Y)
                  .OrderBy(p => CalculateDistance(actualPosition, p))
                  .FirstOrDefault();

               if (cornerCandidate is null)
               {
                  corner = new Coordinate2d(,);
               }
               else
               {
                  corner = cornerCandidate;
               }
            }
            

            targetX = actualPosition.X + RoundNumber((corner.X - actualPosition.X) / 2d);
            targetY = actualPosition.Y - RoundNumber((actualPosition.Y - corner.Y) / 2d);

            break;
         case Directions.R:

            corner = positionsSoFar
                        .Where(p => p.X > actualPosition.X)
                        .OrderBy(p => p.X)
                        .FirstOrDefault() ?? new Coordinate2d(buildingWidth - 1, actualPosition.Y);

            targetX = actualPosition.X + RoundNumber((corner.X - actualPosition.X) / 2d);
            targetY = actualPosition.Y;

            break;
         case Directions.DR:

            corner = positionsSoFar
                        .Where(p => p.X > actualPosition.X)
                        .Where(p => p.Y > actualPosition.Y)
                        .OrderBy(p => CalculateDistance(actualPosition, p))
                        .FirstOrDefault() ?? new Coordinate2d(buildingWidth - 1, buildingHeight - 1);

            targetX = actualPosition.X + RoundNumber((corner.X - actualPosition.X) / 2d);
            targetY = actualPosition.Y + RoundNumber((corner.Y - actualPosition.Y) / 2d);

            break;
         case Directions.D:

            corner = positionsSoFar
                        .Where(p => p.Y > actualPosition.Y)
                        .OrderBy(p => p.Y)
                        .FirstOrDefault() ?? new Coordinate2d(actualPosition.X, buildingHeight - 1);

            targetX = actualPosition.X;
            targetY = actualPosition.Y + RoundNumber((corner.Y - actualPosition.Y) / 2d);

            break;
         case Directions.DL:

            corner = positionsSoFar
                        .Where(p => p.X < actualPosition.X)
                        .Where(p => p.Y > actualPosition.Y)
                        .OrderBy(p => CalculateDistance(actualPosition, p))
                        .FirstOrDefault() ?? new Coordinate2d(0, buildingHeight - 1);

            targetX = actualPosition.X - RoundNumber((actualPosition.X - corner.X) / 2d);
            targetY = actualPosition.Y + RoundNumber((corner.Y - actualPosition.Y) / 2d);

            break;
         case Directions.L:

            corner = positionsSoFar
                        .Where(p => p.X < actualPosition.X)
                        .OrderByDescending(p => p.X)
                        .FirstOrDefault() ?? new Coordinate2d(0, actualPosition.Y);

            targetX = actualPosition.X - RoundNumber((actualPosition.X - corner.X) / 2d);
            targetY = actualPosition.Y;

            break;
         case Directions.UL:

            corner = positionsSoFar
                        .Where(p => p.X < actualPosition.X)
                        .Where(p => p.Y < actualPosition.Y)
                        .OrderBy(p => CalculateDistance(actualPosition, p))
                        .FirstOrDefault() ?? new Coordinate2d(0, 0);

            targetX = actualPosition.X - RoundNumber((actualPosition.X - corner.X) / 2d);
            targetY = actualPosition.Y - RoundNumber((actualPosition.Y - corner.Y) / 2d);

            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
      }

      return new Coordinate2d(targetX, targetY);
   }

   private static int RoundNumber(double value)
   {
      return (int)Math.Ceiling(value);
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   private static double CalculateDistance(Coordinate2d point1, Coordinate2d point2)
   {
      var p1XDiff = point1.X - point2.X;
      var squareOfX = Math.Pow(p1XDiff, 2);
      var p1YDiff = point1.Y - point2.Y;
      var squareOfY = Math.Pow(p1YDiff, 2);

      return Math.Sqrt(squareOfX + squareOfY);
   }
}
