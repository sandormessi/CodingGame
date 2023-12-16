namespace CodingGames.Puzzles.Medium;

using System;
using System.Collections.Generic;
using System.Linq;

public class TheFallEpisode1
{
   #region Constants and Fields

   private const string ThereIsNoWayOutOfThisRoom = "There is no way out of this room.";

   private static readonly Room room0 = new(new[]
   {
      new RoomPath(EnterDirection.None, ExitDirection.None)
   });

   private static readonly Room room1 = new(new[]
   {
      new RoomPath(EnterDirection.Left, ExitDirection.Bottom), 
      new RoomPath(EnterDirection.Top, ExitDirection.Bottom),
      new RoomPath(EnterDirection.Right, ExitDirection.Bottom)
   });

   private static readonly Room room10 = new(new[]
   {
      new RoomPath(EnterDirection.Top, ExitDirection.Left)
   });

   private static readonly Room room11 = new(new[]
   {
      new RoomPath(EnterDirection.Top, ExitDirection.Right)
   });

   private static readonly Room room12 = new(new[]
   {
      new RoomPath(EnterDirection.Right, ExitDirection.Bottom)
   });

   private static readonly Room room13 = new(new[]
   {
      new RoomPath(EnterDirection.Left, ExitDirection.Bottom)
   });

   private static readonly Room room2 = new(new[]
   {
      new RoomPath(EnterDirection.Left, ExitDirection.Right), 
      new RoomPath(EnterDirection.Right, ExitDirection.Left)
   });

   private static readonly Room room3 = new(new[]
   {
      new RoomPath(EnterDirection.Top, ExitDirection.Bottom)
   });

   private static readonly Room room4 = new(new[]
   {
      new RoomPath(EnterDirection.Top, ExitDirection.Left), 
      new RoomPath(EnterDirection.Right, ExitDirection.Bottom)
   });

   private static readonly Room room5 = new(new[]
   {
      new RoomPath(EnterDirection.Top, ExitDirection.Right), 
      new RoomPath(EnterDirection.Left, ExitDirection.Bottom)
   });

   private static readonly Room room6 = new(new[]
   {
      new RoomPath(EnterDirection.Left, ExitDirection.Right), 
      new RoomPath(EnterDirection.Right, ExitDirection.Left)
   });

   private static readonly Room room7 = new(new[]
   {
      new RoomPath(EnterDirection.Top, ExitDirection.Bottom),
      new RoomPath(EnterDirection.Right, ExitDirection.Bottom)
   });

   private static readonly Room room8 = new(new[]
   {
      new RoomPath(EnterDirection.Left, ExitDirection.Bottom),
      new RoomPath(EnterDirection.Right, ExitDirection.Bottom)
   });

   private static readonly Room room9 = new(new[]
   {
      new RoomPath(EnterDirection.Left, ExitDirection.Bottom), 
      new RoomPath(EnterDirection.Top, ExitDirection.Bottom)
   });

   private static readonly IReadOnlyList<Room> Rooms = new List<Room>(12)
   {
      room0,
      room1,
      room2,
      room3,
      room4,
      room5,
      room6,
      room7,
      room8,
      room9,
      room10,
      room11,
      room12,
      room13
   };

   #endregion

   #region Enums

   private enum EnterDirection
   {
      None,

      Top,

      Left,

      Right
   }

   private enum ExitDirection
   {
      None,

      Left,

      Right,

      Bottom
   }

   #endregion

   #region Public Methods and Operators

   public static void Main()
   {
      string[] inputs = ReadInput().Split(' ');

      int width = int.Parse(inputs[0]);
      int height = int.Parse(inputs[1]);

      Room[][] tunnel = new Room[height][];
      for (int i = 0; i < height; i++)
      {
         string line = ReadInput();
         var lineNumbers = line.Split(' ').Select(int.Parse).ToArray();
         tunnel[i] = new Room[width];
         for (var index = 0; index < lineNumbers.Length; index++)
         {
            var lineNumber = lineNumbers[index];
            tunnel[i][index] = Rooms[lineNumber];
         }
      }

      int exit = int.Parse(ReadInput()); // the coordinate along the X axis of the exit (not useful for this first mission, but must be read).

      while (true)
      {
         inputs = ReadInput().Split(' ');

         int positionX = int.Parse(inputs[0]);
         int positionY = int.Parse(inputs[1]);
         string position = inputs[2];

         var enterDirection = Enum.Parse<EnterDirection>(position, true);

         var foundRoom = tunnel[positionY][positionX];

         var foundPaths = foundRoom.RoomPaths.Where(x => x.EnterDirection == enterDirection).ToArray();
         if (!foundPaths.Any())
         {
            throw new InvalidOperationException(ThereIsNoWayOutOfThisRoom);
         }

         if (foundPaths.Length > 1)
         {
            throw new InvalidOperationException("Not trivial choice.");
         }

         var targetPath = foundPaths.First();

         int targetX = positionX;
         int targetY = positionY;

         switch (targetPath.ExitDirection)
         {
            case ExitDirection.None:

               throw new InvalidOperationException(ThereIsNoWayOutOfThisRoom);

            case ExitDirection.Left:

               targetX--;

               break;
            case ExitDirection.Right:

               targetX++;

               break;
            case ExitDirection.Bottom:

               targetY++;

               break;
            default:
               throw new ArgumentOutOfRangeException();
         }

         Console.WriteLine($"{targetX} {targetY}");
      }
   }

   #endregion

   #region Methods

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   #endregion

   private sealed class Room
   {
      #region Constructors and Destructors

      public Room(IEnumerable<RoomPath> roomPaths)
      {
         RoomPaths = roomPaths.ToArray();
      }

      #endregion

      #region Public Properties

      public IReadOnlyList<RoomPath> RoomPaths { get; }

      #endregion
   }

   private sealed class RoomPath
   {
      #region Constructors and Destructors

      public RoomPath(EnterDirection enterDirection, ExitDirection exitDirection)
      {
         EnterDirection = enterDirection;
         ExitDirection = exitDirection;
      }

      #endregion

      #region Public Properties

      public EnterDirection EnterDirection { get; }

      public ExitDirection ExitDirection { get; }

      #endregion
   }
}