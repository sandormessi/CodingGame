namespace CodingGames.Puzzles.Medium;

using System.Diagnostics;

public class BlunderEpisode1
{
   #region Enums

   private enum Directions
   {
      South,

      East,

      West,

      North
   }

   #endregion

   #region Public Methods and Operators

   public static void Main()
   {
      string[] inputs = ReadInput().Split(' ');

      int L = int.Parse(inputs[0]);
      int C = int.Parse(inputs[1]);

      string[] map = new string[L];

      Coordinate2D? startPosition = null;

      for (int i = 0; i < L; i++)
      {
         string row = ReadInput();
         var indexOfStart = row.IndexOf('@');

         if (indexOfStart >= 0)
         {
            startPosition = new Coordinate2D(indexOfStart, i);
         }

         map[i] = row;
      }

      var cityMap = map.Select(x => x.ToCharArray()).ToArray();

      if (startPosition is null)
      {
         throw new InvalidOperationException("Invalid input. There is no start point.");
      }

      StateMachine stateMachine = new(startPosition, Directions.South, cityMap);

      List<Movement> movements = new(64);
      while (stateMachine.GetNextMovement() is { } actualMovement)
      {
         movements.Add(actualMovement);

         var loop = CheckLoop(movements);
         if (loop)
         {
            DebugMovements(movements);
            Console.WriteLine("LOOP");
            return;
         }
      }

      WriteMovements(movements);
   }

   #endregion

   #region Methods

   private static bool CheckLoop(List<Movement> movements)
   {
      return CheckLoop2(movements);

      if (movements.Count < 2)
      {
         return false;
      }

      var actualMovement = movements[^2];
      var nextMovement = movements[^1];

      var sameMovementIndex = movements.FindLastIndex(x =>
         x.NextPosition.Equals(actualMovement.NextPosition) && x.Direction == actualMovement.Direction && !ReferenceEquals(x, actualMovement));

      if (sameMovementIndex >= 0)
      {
         //var sameMovement = movements[sameMovementIndex];
         if (sameMovementIndex + 1 >= movements.Count)
         {
            return false;
         }

         var afterSameMovement = movements[sameMovementIndex + 1];
         return nextMovement.ActualPosition.Equals(afterSameMovement.ActualPosition) && nextMovement.Direction == afterSameMovement.Direction
                                                                                     && nextMovement.BreakerMode == afterSameMovement.BreakerMode;
      }

      return false;
   }

   private static bool CheckLoop2(List<Movement> movements)
   {
      if (movements.Count < 2)
      {
         return false;
      }

      var actualMovement = movements[^1];

      var sameMovements = movements.Where(x =>
         x.NextPosition.Equals(actualMovement.NextPosition) && x.Direction == actualMovement.Direction && !ReferenceEquals(x, actualMovement));

      return sameMovements.Count() > 3;
   }

   private static void DebugMovements(List<Movement> movements)
   {
      foreach (var movement in movements)
      {
         Console.Error.WriteLine(GetDirectionString(movement.Direction));
      }
   }

   private static string GetDirectionString(Directions direction)
   {
      return direction.ToString().ToUpper();
   }

   private static string ReadInput()
   {
      var readInput = Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
      Console.Error.WriteLine(readInput);
      return readInput;
   }

   private static void WriteMovements(List<Movement> movements)
   {
      foreach (var movement in movements)
      {
         Console.WriteLine(GetDirectionString(movement.Direction));
      }
   }

   #endregion

   [DebuggerDisplay("({X},{Y})")]
   private sealed class Coordinate2D : IEquatable<Coordinate2D>
   {
      #region Constructors and Destructors

      public Coordinate2D(int x, int y)
      {
         X = x;
         Y = y;
      }

      #endregion

      #region IEquatable<Coordinate2D> Members

      public bool Equals(Coordinate2D? other)
      {
         if (ReferenceEquals(null, other))
         {
            return false;
         }

         if (ReferenceEquals(this, other))
         {
            return true;
         }

         return X == other.X && Y == other.Y;
      }

      #endregion

      #region Public Properties

      public int X { get; }

      public int Y { get; }

      #endregion

      #region Public Methods and Operators

      public override bool Equals(object? obj)
      {
         return ReferenceEquals(this, obj) || obj is Coordinate2D other && Equals(other);
      }

      public override int GetHashCode()
      {
         return HashCode.Combine(X, Y);
      }

      #endregion
   }

   [DebuggerDisplay("{ActualPosition}, {NextPosition}, {Direction}")]
   private sealed class Movement
   {
      #region Constructors and Destructors

      public Movement(Coordinate2D actualPosition, Coordinate2D nextPosition, Directions direction, bool breakerMode)
      {
         ActualPosition = actualPosition ?? throw new ArgumentNullException(nameof(actualPosition));
         NextPosition = nextPosition ?? throw new ArgumentNullException(nameof(nextPosition));
         Direction = direction;
         BreakerMode = breakerMode;
      }

      #endregion

      #region Public Properties

      public Coordinate2D ActualPosition { get; }

      public bool BreakerMode { get; }

      public Directions Direction { get; }

      public Coordinate2D NextPosition { get; }

      #endregion
   }

   private sealed class StateMachine
   {
      #region Constants and Fields

      private static readonly Directions[] DirectionChangePriority1 = { Directions.South, Directions.East, Directions.North, Directions.West };

      private static readonly Directions[] DirectionChangePriority2 = { Directions.West, Directions.North, Directions.East, Directions.South };

      private readonly char[][] cityMap;

      private readonly Directions startDirection;

      private readonly Coordinate2D startPosition;

      private Movement actualMovement;

      private bool breakerMode;

      private Directions[] DirectionChangePriority = DirectionChangePriority1;

      #endregion

      #region Constructors and Destructors

      public StateMachine(Coordinate2D startPosition, Directions startDirection, char[][] cityMap)
      {
         this.startPosition = startPosition ?? throw new ArgumentNullException(nameof(startPosition));
         this.startDirection = startDirection;
         actualMovement = new Movement(new Coordinate2D(-1, -1), startPosition, this.startDirection, false);

         this.cityMap = cityMap ?? throw new ArgumentNullException(nameof(cityMap));
      }

      #endregion

      #region Public Methods and Operators

      public char GetCell(Coordinate2D position)
      {
         return cityMap[position.Y][position.X];
      }

      public Movement? GetNextMovement()
      {
         var movement = GetMovement();
         if (movement is null)
         {
            return movement;
         }

         return actualMovement = movement;
      }

      public void SetCell(Coordinate2D position, char newCell)
      {
         cityMap[position.Y][position.X] = newCell;
      }

      #endregion

      #region Methods

      private static Movement CalculateMovement(Coordinate2D actualPosition, Directions direction, bool breakerMode)
      {
         Coordinate2D nextPosition = GetPositionFromDirection(actualPosition, direction);
         return new Movement(actualPosition, nextPosition, direction, breakerMode);
      }

      private static Coordinate2D GetPositionFromDirection(Coordinate2D actualPosition, Directions direction)
      {
         switch (direction)
         {
            case Directions.South:
               return new Coordinate2D(actualPosition.X, actualPosition.Y + 1);

            case Directions.East:
               return new Coordinate2D(actualPosition.X + 1, actualPosition.Y);

            case Directions.West:
               return new Coordinate2D(actualPosition.X - 1, actualPosition.Y);

            case Directions.North:
               return new Coordinate2D(actualPosition.X, actualPosition.Y - 1);

            default:
               throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
         }
      }

      private Movement CalculateNextMovementForObstacle(Movement nextMovement)
      {
         int i = -1;

         var nextMovementForObstacle = nextMovement;
         while (true)
         {
            i++;
            var nextCell = GetCell(nextMovementForObstacle.NextPosition);
            if (nextCell == '#')
            {
               nextMovementForObstacle = CalculateMovement(nextMovementForObstacle.ActualPosition, DirectionChangePriority[i], breakerMode);
            }
            else
            {
               if (nextCell == 'X')
               {
                  if (breakerMode)
                  {
                     SetCell(nextMovementForObstacle.NextPosition, ' ');
                     break;
                  }

                  nextMovementForObstacle = CalculateMovement(nextMovementForObstacle.ActualPosition, DirectionChangePriority[i], breakerMode);
               }
               else
               {
                  break;
               }
            }
         }

         return nextMovementForObstacle;
      }

      private void ChangeDirectionPriorities()
      {
         if (ReferenceEquals(DirectionChangePriority, DirectionChangePriority1))
         {
            DirectionChangePriority = DirectionChangePriority2;
         }
         else
         {
            DirectionChangePriority = DirectionChangePriority1;
         }
      }

      private Coordinate2D FindPortal(Coordinate2D actualMovementNextPosition)
      {
         for (int i = 0; i < cityMap.Length; i++)
         {
            for (int j = 0; j < cityMap[i].Length; j++)
            {
               char actualCell = cityMap[i][j];
               if (actualCell == 'T')
               {
                  if (actualMovementNextPosition.X == j && actualMovementNextPosition.Y == i)
                  {
                     continue;
                  }

                  return new Coordinate2D(j, i);
               }
            }
         }

         throw new InvalidOperationException("Could not found other portal.");
      }

      private Movement? GetMovement()
      {
         var actualCell = GetCell(actualMovement.NextPosition);

         var nextMovement = NextMovement(actualCell);
         if (nextMovement is null)
         {
            return null;
         }

         var nextCell = GetCell(nextMovement.NextPosition);
         switch (nextCell)
         {
            case '#':
            case 'X':
               return CalculateNextMovementForObstacle(nextMovement);
         }

         return nextMovement;
      }

      private Movement? NextMovement(char actualCell, bool teleport = false)
      {
         switch (actualCell)
         {
            case ' ':
               return CalculateMovement(actualMovement.NextPosition, actualMovement.Direction, breakerMode);

            case 'S':
               return CalculateMovement(actualMovement.NextPosition, Directions.South, breakerMode);
            case 'E':
               return CalculateMovement(actualMovement.NextPosition, Directions.East, breakerMode);
            case 'N':
               return CalculateMovement(actualMovement.NextPosition, Directions.North, breakerMode);
            case 'W':
               return CalculateMovement(actualMovement.NextPosition, Directions.West, breakerMode);

            case 'B':
               breakerMode = !breakerMode;
               return CalculateMovement(actualMovement.NextPosition, actualMovement.Direction, breakerMode);

            case 'I':
               ChangeDirectionPriorities();
               return CalculateMovement(actualMovement.NextPosition, actualMovement.Direction, breakerMode);

            case 'T':
               if (teleport)
               {
                  return CalculateMovement(actualMovement.NextPosition, actualMovement.Direction, breakerMode);
               }

               var otherPortalPosition = FindPortal(actualMovement.NextPosition);
               actualMovement = CalculateMovement(otherPortalPosition, actualMovement.Direction, breakerMode);
               return actualMovement;

            case '$':
               // Death
               return null;

            case '@':
               return CalculateMovement(startPosition, startDirection, breakerMode);
         }

         throw new InvalidOperationException($"{actualCell}");
      }

      #endregion
   }
}