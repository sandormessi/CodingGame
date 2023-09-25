namespace DoNotPanicEpisode1;

internal class DoNotPanicEpisode1Game
{
   private enum Directions
   {
      Left,

      Right
   }

   private class Floor
   {
      public Floor(int width)
      {
         Width = width;
      }

      public int Width { get; }
      public IList<int> Elevators { get; } = new List<int>();
      public int? ExitPosition { get; set; }
   }

   static void Main12()
   {
      string[] inputs = ReadInput().Split(' ');

      int nbFloors = int.Parse(inputs[0]); // number of floors
      int width = int.Parse(inputs[1]); // width of the area
      int nbRounds = int.Parse(inputs[2]); // maximum number of rounds
      int exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
      int exitPos = int.Parse(inputs[4]); // position of the exit on its floor
      int nbTotalClones = int.Parse(inputs[5]); // number of generated clones
      int nbAdditionalElevators = int.Parse(inputs[6]); // ignore (always zero)
      int nbElevators = int.Parse(inputs[7]); // number of elevators

      List<Floor> floors = new(nbElevators);
      for (var i = 0; i < nbFloors; i++)
      {
         floors.Add(new Floor(width));
      }

      floors[exitFloor].ExitPosition = exitPos;

      for (var i = 0; i < nbElevators; i++)
      {
         inputs = ReadInput().Split(' ');

         int elevatorFloor = int.Parse(inputs[0]);
         int elevatorPos = int.Parse(inputs[1]);

         Floor foundFloor = floors[elevatorFloor];
         foundFloor.Elevators.Add(elevatorPos);
      }

      List<KeyValuePair<Floor, int>> blockedClones = new();

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

         var direction = Enum.Parse<Directions>(directionString, true);

         if (actualFloor.ExitPosition is null)
         {
            int targetPosition = actualFloor.Elevators[0];
            if (targetPosition == clonePos)
            {
               DoNothing();
               continue;
            }

            Directions directionOfExit = CalculateDirectionOfDesiredMovement(targetPosition, clonePos);
            if ((directionOfExit != direction) && !IsBlockedCloneInDirection(actualFloor, direction, blockedClones, clonePos))
            {
               Block(floors, blockedClones, actualFloor, clonePos);
               continue;
            }

            DoNothing();
         }
         else
         {
            int targetPosition = actualFloor.ExitPosition.Value;
            if (targetPosition == clonePos)
            {
               DoNothing();
               continue;
            }

            Directions directionOfExit = CalculateDirectionOfDesiredMovement(targetPosition, clonePos);
            if ((directionOfExit != direction) && !IsBlockedCloneInDirection(actualFloor, direction, blockedClones, clonePos))
            {
               Block(floors, blockedClones, actualFloor, clonePos);
               continue;
            }

            DoNothing();
         }
      }
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

   private static void Block(IList<Floor> floors, ICollection<KeyValuePair<Floor, int>> blockedClones, Floor actualFloor,
      int clonePos)
   {
      int indexOfActualFloor = floors.IndexOf(actualFloor);
      indexOfActualFloor--;
      if ((indexOfActualFloor >= 0) && floors[indexOfActualFloor].Elevators.Contains(clonePos))
      {
         // Do not block the elevator
         DoNothing();
         return;
      }

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