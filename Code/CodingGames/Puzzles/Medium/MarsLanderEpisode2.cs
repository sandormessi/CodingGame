namespace CodingGames.Puzzles.Medium;

using System;
using System.Collections.Generic;
using System.Linq;

public class MarsLanderEpisode2
{
   private sealed class Coordinate2D
   {
      public Coordinate2D(int x, int y)
      {
         X = x;
         Y = y;
      }

      public int X { get; }

      public int Y { get; }

      public override string ToString()
      {
         return $"{X}, {Y}";
      }
   }

   public static void Main()
   {
      string[] inputs;
      int surfaceN = int.Parse(ReadInput()); // the number of points used to draw the surface of Mars.
     
      List<Coordinate2D> marsSurfacePoints = new(surfaceN);
      for (int i = 0; i < surfaceN; i++)
      {
         inputs = ReadInput().Split(' ');
         int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
         int landY = int.Parse(inputs[1]); // Y coordinate of a surface point.
         
         // By linking all the points together in a sequential fashion, you form the surface of Mars.
         
         marsSurfacePoints.Add(new Coordinate2D(landX, landY));
      }

      var flatGroundCoordinates = FindFlatGround(marsSurfacePoints);

      int roundCount = 1;
      bool targetAreaReached = false;
      while (true)
      {
         inputs = ReadInput().Split(' ');
         int X = int.Parse(inputs[0]);
         int Y = int.Parse(inputs[1]);

         Coordinate2D marsLanderPosition = new(X, Y);

         var closestFlatGround = FindClosestFlatGround(marsLanderPosition, flatGroundCoordinates).OrderBy(x => x.X).ToArray();

         var closestFlatGroundCoordinate = closestFlatGround.MinBy(x => Math.Abs(x.X - marsLanderPosition.X));

         var distance = closestFlatGroundCoordinate.X - marsLanderPosition.X;

         int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
         int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
         int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
         
         var earlyGoingDown = 500;
         int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
         int power = int.Parse(inputs[6]); // the thrust power (0 to 4).

         var firstCoordinateOfFlatGround = closestFlatGround.First();
         var lastCoordinateOfFlatGround= closestFlatGround.Last();

         Console.Error.WriteLine($"Flag ground: {firstCoordinateOfFlatGround.X}, {lastCoordinateOfFlatGround.X}");

         var targetXCoordinate = ((lastCoordinateOfFlatGround.X - firstCoordinateOfFlatGround.X) / 2) + firstCoordinateOfFlatGround.X;
         
         const int horizontalSpeedLimit = 50;

         Console.Error.WriteLine($"Target X position: {targetXCoordinate}.");

         var distanceFromTarget = targetXCoordinate - marsLanderPosition.X;
         if (Math.Abs(distanceFromTarget) < 50)
         {
            Console.WriteLine($"0 {4}");
         }
         else
         {
            Console.Error.WriteLine($"Distance from target: {distanceFromTarget}.");
            if (distanceFromTarget < 0)
            {
               // go LEFT
               if (Math.Abs(hSpeed) > horizontalSpeedLimit)
               {
                  Console.WriteLine("-45 4");
               }
               else
               {
                  Console.WriteLine("45 4");
               }
            }
            else if (distanceFromTarget > 0)
            {
               // go RIGHT
               if (Math.Abs(hSpeed) > horizontalSpeedLimit)
               {
                  Console.WriteLine("45 4");
               }
               else
               {
                  Console.WriteLine("-45 4");
               }
            }
            else
            {
               // go straight down
               Console.WriteLine($"0 {4}");
            }
         }

         //if (Math.Abs(distanceFromTarget) < earlyGoingDown)
         //{
         //   Console.Error.WriteLine("Target area reached.");
         //   targetAreaReached = true;
         //}
         //else if(!targetAreaReached)
         //{
          
         //   if (distance < 0)
         //   {
         //      // Have to move to the LEFT
         //      Console.Error.WriteLine("Move LEFT");
         //      if (Math.Abs( hSpeed) > horizontalSpeedLimit)
         //      {
         //         Console.WriteLine("-30 4");
         //      }
         //      else
         //      {
         //         Console.WriteLine("30 4");
         //      }
         //   }
         //   else if (distance > 0)
         //   {
         //      // Have to move to the RIGHT
         //      Console.Error.WriteLine("Move RIGHT");

         //      if (Math.Abs(hSpeed) > horizontalSpeedLimit)
         //      {
         //         Console.WriteLine("30 4");
         //      }
         //      else
         //      {
         //         Console.WriteLine("-30 4");
         //      }
         //   }
         //   else
         //   {
         //      MoveDown(rotate, hSpeed, vSpeed, power, marsLanderPosition, closestFlatGround);
         //   }
         //}

         //if (targetAreaReached)
         //{
         //   MoveDown(rotate, hSpeed, vSpeed, power, marsLanderPosition, closestFlatGround);
         //}

         roundCount++;
      }
   }

   private static void MoveDown(int rotate, int hSpeed, int vSpeed, int power, Coordinate2D marsLanderPosition,
      IReadOnlyList<Coordinate2D> closestFlatGround)
   {
      // Have to move DOWN
      Console.Error.WriteLine("Move DOWN");

      var middle = (closestFlatGround.Last().X - closestFlatGround.First().X) / 2;

      power = 4;
      if (Math.Abs(hSpeed) <= 20)
      {
         if (Math.Abs(vSpeed) > 40)
         {
            Console.Error.WriteLine("Horizontal speed okay, going down.");
            Console.WriteLine($"0 {power}");
         }
         else
         {
            power--;
            if (power < 1)
            {
               power = 1;
            }

            Console.WriteLine($"0 {power}");
         }
      }
      else
      {
         if (hSpeed > 0)
         {
            Console.Error.WriteLine("Going from right, rotate to left to slow down.");
            Console.WriteLine($"45 {power}");
         }
         else if (hSpeed < 0)
         {
            Console.Error.WriteLine("Going from left, rotate to right to slow down.");
            Console.WriteLine($"-45 {power}");
         }
         else
         {
            Console.Error.WriteLine("Horizontal speed okay, going down.");
            Console.WriteLine($"0 {power}");
         }
      }
   }
   
   private static IReadOnlyList<Coordinate2D> FindClosestFlatGround(Coordinate2D marsLanderPosition, IReadOnlyList<IReadOnlyList<Coordinate2D>> flatGroundCoordinates)
   {
      var closestFlatGround = flatGroundCoordinates.MinBy(flatGround =>
      {
         return flatGround.MinBy(flatGroundCoordinate => Math.Abs(flatGroundCoordinate.X - marsLanderPosition.X));
      });


      return new[]
      {
         closestFlatGround.MinBy(x => x.X),
         closestFlatGround.MaxBy(x => x.X)
      };
   }

   private static IReadOnlyList<IReadOnlyList<Coordinate2D>> FindFlatGround(IReadOnlyList<Coordinate2D> marsSurfacePoints)
   {
      List<IReadOnlyList<Coordinate2D>> flatGrounds = new ();
      for (int i = 0; i < marsSurfacePoints.Count; i++)
      {
         var actualPoint = marsSurfacePoints[i];
         List<Coordinate2D> flatGround = new(64);
         while (i < marsSurfacePoints.Count && actualPoint.Y == marsSurfacePoints[i].Y)
         {
            flatGround.Add(marsSurfacePoints[i]);
            i++;
         }

         i--;
         if (flatGround.Count > 1)
         {
            flatGround.OrderBy(x => x.X);
            flatGrounds.Add(flatGround);
         }
      }

      return flatGrounds; 
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }
}