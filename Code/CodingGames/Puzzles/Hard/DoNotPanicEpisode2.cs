namespace CodingGames.Puzzles.Hard;

using System;
using System.Collections.Generic;
using System.Linq;

internal class DoNotPanicEpisode2
{
   #region Enums

   private enum Directions
   {
      Left,

      Right
   }

   #endregion

   #region Methods

   private static void Block(ICollection<KeyValuePair<Floor, int>> blockedClones, Floor actualFloor, int clonePos)
   {
      blockedClones.Add(new KeyValuePair<Floor, int>(actualFloor, clonePos));
      Console.WriteLine("BLOCK");
   }

   private static void BuildElevator(Floor actualFloor, int clonePos)
   {
      actualFloor.Elevators.Add(clonePos);
      Console.WriteLine("ELEVATOR");
   }

   private static Directions CalculateDirectionOfDesiredMovement(int actualFloorExitPosition, int clonePos)
   {
      if (clonePos > actualFloorExitPosition)
      {
         return Directions.Left;
      }

      return Directions.Right;
   }

   private static void DoNothing()
   {
      Console.WriteLine("WAIT");
   }

   private static void FindPositionForElevatorsToBuild(IReadOnlyList<Floor> floors, int elevatorsCanBeBuilt, int generatorPosition)
   {
      if (elevatorsCanBeBuilt <= 0)
      {
         return;
      }

      // TODO if elevator is built on a level where other elevators are then other levels need to be checked again (TargetPosition)

      IReadOnlyList<Floor> orderedFloors =
         SelectFloorsForBuildingElevator(floors).Take(elevatorsCanBeBuilt).OrderByDescending(x => x.Level).ToArray();
      foreach (Floor orderedFloor in orderedFloors)
      {
         if (orderedFloor.Level == 0)
         {
            orderedFloor.TargetPosition = generatorPosition;
         }
         else
         {
            Floor previousLevel = floors[orderedFloor.Level + 1];
            orderedFloor.TargetPosition = previousLevel.TargetPosition;

            int nextLevel = orderedFloor.Level - 1;
            if (orderedFloors.All(x => x.Level != nextLevel))
            {
               Floor nextFloor = floors.First(x => x.Level == nextLevel);
               if (nextFloor.Elevators.Any())
               {
                  nextFloor.TargetPosition = nextFloor.Elevators.MinBy(x => Math.Abs(x - orderedFloor.TargetPosition));
               }

               Console.Error.WriteLine($"Neighbor Floor '{nextFloor.Level}' TargetPosition has been set to: {nextFloor.TargetPosition}.");
            }

            Console.Error.WriteLine($"Floor '{orderedFloor.Level}' TargetPosition has been set to: {orderedFloor.TargetPosition}.");
         }
      }
   }

   private static void FindTargetPositions(List<Floor> floors, int elevatorsCanBeBuilt, int generatorPosition)
   {
      Floor exitFloor = floors[^1];
      int targetPosition = exitFloor.ExitPosition.Value;
      exitFloor.TargetPosition = targetPosition;

      double actualMedian = targetPosition;
      for (int i = floors.Count - 2; i >= 0; i--)
      {
         Floor actualFloor = floors[i];
         if (!actualFloor.Elevators.Any())
         {
            if (elevatorsCanBeBuilt < 0)
            {
               throw new InvalidOperationException("No more elevators can be built.");
            }

            elevatorsCanBeBuilt--;
            actualFloor.TargetPosition = targetPosition;
         }
         else
         {
            int actualFloorElevatorToUse = actualFloor.Elevators.MinBy(x => Math.Abs(x - targetPosition));

            double distanceFromMedian = Math.Abs(actualMedian - actualFloorElevatorToUse);
            actualFloor.SaveAmount = distanceFromMedian;

            actualFloor.TargetPosition = actualFloorElevatorToUse;
         }

         actualMedian = (double)targetPosition / actualFloor.TargetPosition;

         targetPosition = actualFloor.TargetPosition;
      }

      Floor firstFloor = floors[0];
      if (firstFloor.Elevators.Count > 1)
      {
         int[] elevatorsOnTheLeft = firstFloor.Elevators.Where(x => x < generatorPosition).ToArray();
         int[] elevatorsOnTheRight = firstFloor.Elevators.Where(x => x > generatorPosition).ToArray();

         if (elevatorsOnTheLeft.Any() && elevatorsOnTheRight.Any())
         {
            int first = elevatorsOnTheLeft.Max();
            int second = elevatorsOnTheRight.Min();

            int stepsForFirst = Math.Abs(first - generatorPosition) + /* Plus 3 steps due to clone generator */ 3;
            int stepsForSecond = Math.Abs(second - generatorPosition);

            if (stepsForFirst < stepsForSecond)
            {
               firstFloor.TargetPosition = first;
            }
            else
            {
               firstFloor.TargetPosition = second;
            }
         }
      }

      FindPositionForElevatorsToBuild(floors, elevatorsCanBeBuilt, generatorPosition);

      foreach (Floor floor in floors)
      {
         Console.Error.WriteLine($"Floor {floor.Level}, TargetPosition: {floor.TargetPosition}.");
      }
   }

   private static List<Floor> GetFloors(int nbElevators, int exitFloor, int exitPos)
   {
      List<Floor> floors = new(nbElevators);
      for (var i = 0; i <= exitFloor; i++)
      {
         floors.Add(new Floor(i));
      }

      floors[exitFloor].ExitPosition = exitPos;

      for (var i = 0; i < nbElevators; i++)
      {
         string[] inputs = ReadInput().Split(' ');

         int elevatorFloor = int.Parse(inputs[0]);
         int elevatorPos = int.Parse(inputs[1]);

         Floor foundFloor = floors[elevatorFloor];
         foundFloor.Elevators.Add(elevatorPos);
      }

      return floors;
   }

   private static bool IsBlockedCloneInDirection(Floor actualFloor, Directions direction, IReadOnlyList<KeyValuePair<Floor, int>> blockedClones,
      int clonePos)
   {
      KeyValuePair<Floor, int>[] foundFloors = blockedClones.Where(x => x.Key.Equals(actualFloor)).ToArray();
      if (!foundFloors.Any())
      {
         return false;
      }

      return direction switch
      {
         Directions.Left => foundFloors.Any(x => x.Value < clonePos),
         Directions.Right => foundFloors.Any(x => x.Value > clonePos),
         _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
      };
   }

   private static void Main()
   {
      string[] inputs = ReadInput().Split(' ');

      int nbFloors = int.Parse(inputs[0]);
      int width = int.Parse(inputs[1]);
      int nbRounds = int.Parse(inputs[2]);
      int exitFloor = int.Parse(inputs[3]);
      int exitPos = int.Parse(inputs[4]);
      int nbTotalClones = int.Parse(inputs[5]);
      int nbAdditionalElevators = int.Parse(inputs[6]);
      int nbElevators = int.Parse(inputs[7]);

      List<Floor> floors = GetFloors(nbElevators, exitFloor, exitPos);

      List<KeyValuePair<Floor, int>> blockedClones = new();

      var isFirstTurn = true;
      while (true)
      {
         inputs = ReadInput().Split(' ');

         int cloneFloor = int.Parse(inputs[0]);

         if (cloneFloor == -1)
         {
            DoNothing();
            continue;
         }

         Floor actualFloor = floors[cloneFloor];

         int clonePos = int.Parse(inputs[1]);
         string directionString = inputs[2];

         if (isFirstTurn)
         {
            FindTargetPositions(floors, nbAdditionalElevators, clonePos);
            isFirstTurn = false;
         }

         var direction = Enum.Parse<Directions>(directionString, true);

         int targetPosition = actualFloor.TargetPosition;
         if (targetPosition == clonePos)
         {
            if (!actualFloor.Elevators.Contains(targetPosition) || (!actualFloor.Elevators.Any() && !actualFloor.ExitPosition.HasValue))
            {
               BuildElevator(actualFloor, clonePos);
            }
            else
            {
               DoNothing();
            }

            continue;
         }

         Directions directionOfExit = CalculateDirectionOfDesiredMovement(targetPosition, clonePos);
         if ((directionOfExit != direction) && !IsBlockedCloneInDirection(actualFloor, direction, blockedClones, clonePos))
         {
            Block(blockedClones, actualFloor, clonePos);
            continue;
         }

         DoNothing();
      }
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   private static IEnumerable<Floor> SelectFloorsForBuildingElevator(IReadOnlyList<Floor> floors)
   {
      for (int i = floors.Count - 2; i >= 0; i--)
      {
         Floor previousFloor = floors[i + 1];
         Floor actualFloor = floors[i];

         var saveAmount = Math.Abs(actualFloor.TargetPosition - previousFloor.TargetPosition);
         actualFloor.SaveAmount = saveAmount;
      }

      return floors.OrderByDescending(x => x.SaveAmount);
   }

   #endregion

   private class Floor
   {
      #region Constructors and Destructors

      public Floor(int level)
      {
         Level = level;
      }

      #endregion

      #region Public Properties

      public IList<int> Elevators { get; } = new List<int>();

      public int? ExitPosition { get; set; }

      public int Level { get; }

      public double SaveAmount { get; set; } = int.MinValue;

      public int TargetPosition { get; set; } = int.MinValue;

      #endregion
   }
}