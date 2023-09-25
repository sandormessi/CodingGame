namespace DoNotPanicEpisode2;

internal class DoNotPanicEpisode2Game
{
   private enum Directions
   {
      Left,

      Right
   }

   private class Floor
   {
      public Floor(int level)
      {
         Level = level;
      }

      public int Level { get; }

      public IList<int> Elevators { get; } = new List<int>();

      public int? ExitPosition { get; set; }

      public int ElevatorToUse { get; set; } = int.MinValue;

      public int DistanceFromGenerator { get; set; } = int.MinValue;
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
            FindElevatorsToUse(floors, nbAdditionalElevators, clonePos);
            isFirstTurn = false;
         }

         var direction = Enum.Parse<Directions>(directionString, true);

         int targetPosition = CalculateTargetPosition(floors, actualFloor, clonePos);
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
         if (directionOfExit != direction && !IsBlockedCloneInDirection(actualFloor, direction, blockedClones, clonePos))
         {
            Block(blockedClones, actualFloor, clonePos);
            continue;
         }

         DoNothing();
      }
   }

   private static void FindElevatorsToUse(List<Floor> floors, int elevatorsCanBeBuilt, int generatorPosition)
   {
      Floor exitFloor = floors[^1];
      int targetPosition = exitFloor.ExitPosition.Value;
      exitFloor.ElevatorToUse = targetPosition;

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
            actualFloor.ElevatorToUse = targetPosition;
         }
         else
         {
            int actualFloorElevatorToUse = actualFloor.Elevators.MinBy(x => Math.Abs(x - targetPosition));
            int distanceFromGenerator = Math.Abs(generatorPosition - actualFloorElevatorToUse);
            actualFloor.DistanceFromGenerator = distanceFromGenerator;
            actualFloor.ElevatorToUse = actualFloorElevatorToUse;
         }

         targetPosition = actualFloor.ElevatorToUse;
      }

      Floor firstFloor = floors[0];
      if (firstFloor.Elevators.Count > 1)
      {
         var elevatorsOnTheLeft = firstFloor.Elevators.Where(x => x < generatorPosition).ToArray();
         var elevatorsOnTheRight = firstFloor.Elevators.Where(x => x > generatorPosition).ToArray();

         if (elevatorsOnTheLeft.Any() && elevatorsOnTheRight.Any())
         {
            var first = elevatorsOnTheLeft.MinBy(x => Math.Abs(x - generatorPosition));
            var second = elevatorsOnTheRight.MinBy(x => Math.Abs(x - generatorPosition));

            var stepsForFirst = Math.Abs(first - generatorPosition) + 3;
            var stepsForSecond = Math.Abs(second - generatorPosition);

            if (stepsForFirst < stepsForSecond)
            {
               firstFloor.ElevatorToUse = first;
            }
            else
            {
               firstFloor.ElevatorToUse = second;
            }
         }
      }

      var orderedFloors = floors.OrderByDescending(x => x.DistanceFromGenerator).ToList();
      for (var i = elevatorsCanBeBuilt; i > 0; i--)
      {
         var greatestDistance = orderedFloors.First();
         if (greatestDistance.Level == 0)
         {
            greatestDistance.ElevatorToUse = generatorPosition;
         }
         else
         {
            if (greatestDistance.Level > 0)
            {
               var previousLevel = floors[greatestDistance.Level - 1];
               greatestDistance.ElevatorToUse = previousLevel.ElevatorToUse;
            }
            else
            {
               var nextLevel = floors[greatestDistance.Level + 1];
               greatestDistance.ElevatorToUse = nextLevel.ElevatorToUse;
            }
         }
       
         
         orderedFloors.RemoveAt(0);
      }

      foreach (Floor floor in floors)
      {
         Console.Error.WriteLine($"Floor {floor.Level}, TargetPosition: {floor.ElevatorToUse}.");
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

   private static int CalculateTargetPosition(IList<Floor> floors, Floor actualFloor, int clonePos)
   {
      return actualFloor.ElevatorToUse;
   }

   private static void BuildElevator(Floor actualFloor, int clonePos)
   {
      actualFloor.Elevators.Add(clonePos);
      Console.WriteLine("ELEVATOR");
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

   private static void Block(ICollection<KeyValuePair<Floor, int>> blockedClones, Floor actualFloor, int clonePos)
   {
      blockedClones.Add(new KeyValuePair<Floor, int>(actualFloor, clonePos));
      Console.WriteLine("BLOCK");
   }

   private static void DoNothing()
   {
      Console.WriteLine("WAIT");
   }

   private static Directions CalculateDirectionOfDesiredMovement(int actualFloorExitPosition, int clonePos)
   {
      if (clonePos > actualFloorExitPosition)
      {
         return Directions.Left;
      }

      return Directions.Right;
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }
}