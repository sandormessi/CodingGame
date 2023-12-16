namespace CodingGames.Puzzles.Medium;

using System;
using System.Collections.Generic;
using System.Linq;

public class ShadowOfTheKnightEpisode1
{
   #region Enums

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

   #endregion

   #region Public Methods and Operators

   public static void Main()
   {
      string[] inputs = ReadInput().Split(' ');

      int buildingWidth = int.Parse(inputs[0]);
      int buildingHeight = int.Parse(inputs[1]);

      int maximumStep = int.Parse(ReadInput());

      inputs = ReadInput().Split(' ');

      int startX = int.Parse(inputs[0]);
      int startY = int.Parse(inputs[1]);

      List<Coordinate2D> positions = new();

      Coordinate2D actualPosition = new(startX, startY);

      while (true)
      {
         string bombDirection = ReadInput();

         var inputConvertedToEnum = (Directions)Enum.Parse(typeof(Directions), bombDirection, true);

         Coordinate2D nextPosition = CalculateCenterOfTargetSquare(actualPosition, positions, inputConvertedToEnum, buildingWidth, buildingHeight);

         positions.Add(nextPosition);

         actualPosition = nextPosition;

         WriteCoordinates(nextPosition);
      }
   }

   #endregion

   #region Methods

   private static Coordinate2D CalculateCenterOfTargetSquare(Coordinate2D actualPosition, IReadOnlyList<Coordinate2D> positionsSoFar,
      Directions direction, int buildingWidth, int buildingHeight)
   {
      Coordinate2D corner;

      int targetX = -1;
      int targetY = -1;

      switch (direction)
      {
         case Directions.None:
            break;
         case Directions.U:

            corner = new Coordinate2D(actualPosition.X, positionsSoFar.Where(p => p.Y < actualPosition.Y).MaxBy(p => p.Y)?.Y ?? 0);

            targetX = actualPosition.X;
            targetY = actualPosition.Y - RoundNumber((actualPosition.Y - corner.Y) / 2d);

            break;
         case Directions.D:

            corner = new Coordinate2D(actualPosition.X, positionsSoFar.Where(p => p.Y > actualPosition.Y).MinBy(p => p.Y)?.Y ?? buildingHeight - 1);

            targetX = actualPosition.X;
            targetY = actualPosition.Y + RoundNumber((corner.Y - actualPosition.Y) / 2d);

            break;
         case Directions.L:

            corner = new Coordinate2D(positionsSoFar.Where(p => p.X < actualPosition.X).MaxBy(p => p.X)?.X ?? 0, actualPosition.Y);

            targetX = actualPosition.X - RoundNumber((actualPosition.X - corner.X) / 2d);
            targetY = actualPosition.Y;

            break;
         case Directions.R:

            corner = new Coordinate2D(positionsSoFar.Where(p => p.X > actualPosition.X).MinBy(p => p.X)?.X ?? buildingWidth - 1, actualPosition.Y);

            targetX = actualPosition.X - RoundNumber((actualPosition.X - corner.X) / 2d);
            targetY = actualPosition.Y;

            break;
         case Directions.UR:

            corner = new Coordinate2D(positionsSoFar.Where(p => p.X > actualPosition.X).MinBy(p => p.X)?.X ?? buildingWidth - 1,
               positionsSoFar.Where(p => p.Y < actualPosition.Y).MaxBy(p => p.Y)?.Y ?? 0);

            targetX = actualPosition.X + RoundNumber((corner.X - actualPosition.X) / 2d);
            targetY = actualPosition.Y - RoundNumber((actualPosition.Y - corner.Y) / 2d);

            break;
         case Directions.UL:

            corner = new Coordinate2D(positionsSoFar.Where(p => p.X < actualPosition.X).MaxBy(p => p.X)?.X ?? 0,
               positionsSoFar.Where(p => p.Y < actualPosition.Y).MaxBy(p => p.Y)?.Y ?? 0);

            targetX = actualPosition.X - RoundNumber((actualPosition.X - corner.X) / 2d);
            targetY = actualPosition.Y - RoundNumber((actualPosition.Y - corner.Y) / 2d);

            break;
         case Directions.DR:

            corner = new Coordinate2D(positionsSoFar.Where(p => p.X > actualPosition.X).MinBy(p => p.X)?.X ?? buildingWidth - 1,
               positionsSoFar.Where(p => p.Y > actualPosition.Y).MinBy(p => p.Y)?.Y ?? buildingHeight - 1);

            targetX = actualPosition.X + RoundNumber((corner.X - actualPosition.X) / 2d);
            targetY = actualPosition.Y + RoundNumber((corner.Y - actualPosition.Y) / 2d);

            break;
         case Directions.DL:

            corner = new Coordinate2D(positionsSoFar.Where(p => p.X < actualPosition.X).MaxBy(p => p.X)?.X ?? 0,
               positionsSoFar.Where(p => p.Y > actualPosition.Y).MinBy(p => p.Y)?.Y ?? buildingHeight - 1);

            targetX = actualPosition.X - RoundNumber((actualPosition.X - corner.X) / 2d);
            targetY = actualPosition.Y + RoundNumber((corner.Y - actualPosition.Y) / 2d);

            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
      }

      return new Coordinate2D(targetX, targetY);
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   private static int RoundNumber(double value)
   {
      return (int)Math.Ceiling(value);
   }

   private static void WriteCoordinates(Coordinate2D point)
   {
      Console.WriteLine($"{point.X} {point.Y}");
   }

   #endregion

   private sealed class Coordinate2D
   {
      #region Constructors and Destructors

      public Coordinate2D(int x, int y)
      {
         X = x;
         Y = y;
      }

      #endregion

      #region Public Properties

      public int X { get; }

      public int Y { get; }

      #endregion
   }
}