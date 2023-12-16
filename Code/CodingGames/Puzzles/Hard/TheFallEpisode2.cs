namespace CodingGames.Puzzles.Hard;

using System;
using System.Collections.Generic;
using System.Linq;

public class TheFallEpisode2
{
    #region Constants and Fields

    private const string ThereIsNoWayOutOfThisRoom = "There is no way out of this room.";

    private static readonly Room room0 = new(0, new[]
    {
      new RoomPath(EnterDirection.None, ExitDirection.None)
   });

    private static readonly Room room1 = new(1, new[]
    {
      new RoomPath(EnterDirection.Left, ExitDirection.Bottom),
      new RoomPath(EnterDirection.Top, ExitDirection.Bottom),
      new RoomPath(EnterDirection.Right, ExitDirection.Bottom)
   });

    private static readonly Room room2 = new(2, new[]
    {
      new RoomPath(EnterDirection.Left, ExitDirection.Right),
      new RoomPath(EnterDirection.Right, ExitDirection.Left)
   });

    private static readonly Room room3 = new(3, new[]
    {
      new RoomPath(EnterDirection.Top, ExitDirection.Bottom)
   });

    private static readonly Room room4 = new(4, new[]
    {
      new RoomPath(EnterDirection.Top, ExitDirection.Left),
      new RoomPath(EnterDirection.Right, ExitDirection.Bottom)
   });

    private static readonly Room room5 = new(5, new[]
    {
      new RoomPath(EnterDirection.Top, ExitDirection.Right),
      new RoomPath(EnterDirection.Left, ExitDirection.Bottom)
   });

    private static readonly Room room6 = new(6, new[]
    {
      new RoomPath(EnterDirection.Left, ExitDirection.Right),
      new RoomPath(EnterDirection.Right, ExitDirection.Left)
   });

    private static readonly Room room7 = new(7, new[]
    {
      new RoomPath(EnterDirection.Top, ExitDirection.Bottom),
      new RoomPath(EnterDirection.Right, ExitDirection.Bottom)
   });

    private static readonly Room room8 = new(8, new[]
    {
      new RoomPath(EnterDirection.Left, ExitDirection.Bottom),
      new RoomPath(EnterDirection.Right, ExitDirection.Bottom)
   });

    private static readonly Room room9 = new(9, new[]
    {
      new RoomPath(EnterDirection.Left, ExitDirection.Bottom),
      new RoomPath(EnterDirection.Top, ExitDirection.Bottom)
   });

    private static readonly Room room10 = new(10, new[]
    {
      new RoomPath(EnterDirection.Top, ExitDirection.Left)
   });

    private static readonly Room room11 = new(11, new[]
    {
      new RoomPath(EnterDirection.Top, ExitDirection.Right)
   });

    private static readonly Room room12 = new(12, new[]
    {
      new RoomPath(EnterDirection.Right, ExitDirection.Bottom)
   });

    private static readonly Room room13 = new(13, new[]
    {
      new RoomPath(EnterDirection.Left, ExitDirection.Bottom)
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
        SetRoomRotations();

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
                bool canBeRotated = true;
                if (lineNumber < 0)
                {
                    lineNumber = -lineNumber;
                    canBeRotated = false;
                }

                var room = Rooms[lineNumber].Copy();
                room.CanBeRotated = canBeRotated;
                tunnel[i][index] = room;
            }
        }

        int exit = int.Parse(ReadInput()); // the coordinate along the X axis of the exit (not useful for this first mission, but must be read).

        while (true)
        {
            inputs = ReadInput().Split(' ');

            int R = int.Parse(ReadInput()); // the number of rocks currently in the grid.

            for (int i = 0; i < R; i++)
            {
                inputs = ReadInput().Split(' ');
                int XR = int.Parse(inputs[0]);
                int YR = int.Parse(inputs[1]);

                string POSR = inputs[2];

                //tunnel[YR][YR].CanBeRotated = false;
            }

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

            Room nextRoom;

            EnterDirection targetEnterDirection;
            ExitDirection targetExitDirection;

            switch (targetPath.ExitDirection)
            {
                case ExitDirection.None:

                    throw new InvalidOperationException(ThereIsNoWayOutOfThisRoom);

                case ExitDirection.Left:

                    targetX--;
                    nextRoom = tunnel[targetY][targetX];

                    targetEnterDirection = EnterDirection.Right;
                    targetExitDirection = ExitDirection.Bottom;

                    break;
                case ExitDirection.Right:

                    targetX++;
                    nextRoom = tunnel[targetY][targetX];

                    targetEnterDirection = EnterDirection.Left;
                    targetExitDirection = ExitDirection.Bottom;

                    break;
                case ExitDirection.Bottom:

                    targetY++;
                    nextRoom = tunnel[targetY][targetX];

                    targetEnterDirection = EnterDirection.Top;
                    targetExitDirection = ExitDirection.Bottom;

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (!nextRoom.RoomPaths.Any(x => x.EnterDirection == targetEnterDirection && x.ExitDirection == targetExitDirection))
            {
                bool isLeft = nextRoom.RotateLeft.RoomPaths.Any(x => x.EnterDirection == targetEnterDirection && x.ExitDirection == targetExitDirection);
                RotateRoom(targetX, targetY, isLeft);
            }
            else
            {
                Wait();
            }
        }
    }

    private static void SetRoomRotations()
    {
        room0.RotateLeft = room0;
        room0.RotateRight = room0;

        room1.RotateRight = room1;
        room1.RotateLeft = room1;

        room2.RotateRight = room3;
        room2.RotateLeft = room3;

        room3.RotateRight = room2;
        room3.RotateLeft = room2;

        room4.RotateRight = room5;
        room4.RotateLeft = room5;

        room5.RotateRight = room4;
        room5.RotateLeft = room4;

        room6.RotateRight = room7;
        room6.RotateLeft = room9;

        room7.RotateRight = room8;
        room7.RotateLeft = room6;

        room8.RotateRight = room9;
        room8.RotateLeft = room7;

        room9.RotateRight = room6;
        room9.RotateLeft = room8;

        room10.RotateRight = room11;
        room10.RotateLeft = room13;

        room11.RotateRight = room12;
        room11.RotateLeft = room10;

        room12.RotateRight = room13;
        room12.RotateLeft = room11;

        room13.RotateRight = room10;
        room13.RotateLeft = room12;
    }

    private static void RotateRoom(int roomX, int roomY, bool isLeft)
    {
        string direction = "LEFT";
        if (!isLeft)
        {
            direction = "RIGHT";
        }

        Console.WriteLine($"{roomX} {roomY} {direction}");
    }

    private static void Wait()
    {
        Console.WriteLine("WAIT");
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

        public Room(int id, IEnumerable<RoomPath> roomPaths)
        {
            Id = id;
            RoomPaths = roomPaths.ToArray();
        }

        #endregion

        #region Public Properties

        public IReadOnlyList<RoomPath> RoomPaths { get; }

        public int Id { get; }

        public bool CanBeRotated { get; set; }

        #endregion

        public Room Copy()
        {
            return new Room(Id, RoomPaths)
            {
                RotateLeft = RotateLeft,
                RotateRight = RotateRight
            };
        }

        public Room RotateRight { get; set; }
        public Room RotateLeft { get; set; }
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