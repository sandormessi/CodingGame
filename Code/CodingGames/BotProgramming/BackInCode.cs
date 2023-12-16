namespace CodingGames.BotProgramming;

using System;
using System.Collections.Generic;
using System.Linq;

public class BackInCode
{
   #region Constants and Fields

   private const int FieldHeight = 20;

   #endregion

   #region Public Methods and Operators

   public static void Main()
   {
      int opponentCount = int.Parse(ReadInput());

      GameData? gameData = null;
      while (true)
      {
         gameData = GetGameData(opponentCount, gameData);

         ExecuteLogic(gameData);
      }
   }

   #endregion

   #region Methods

   private static void DebugMessage(string message)
   {
      Console.Error.WriteLine(message);
   }

   private static void ExecuteCommand(Coordinate2D position)
   {
      Console.WriteLine($"{position.X} {position.Y}");
   }

   private static bool origoReached;
   private static void ExecuteLogic(GameData gameData)
   {
      if (!origoReached && gameData.MyPlayer.Position is not { X: 0, Y: 0 })
      {
         ExecuteCommand(new Coordinate2D(0, 0));
         DebugMessage("Going toward Origo.");
      }
      else
      {
         origoReached = true;
         DebugMessage("Origo reached.");
         var height = gameData.GameField.LineCount;
         var width = gameData.GameField[0].Count;

         var targetX = gameData.MyPlayer.Position.X + 1;
         var targetY = gameData.MyPlayer.Position.Y;
         if (targetX == width)
         {
            targetX = 0;
            targetY++;
         }

         if (targetY == height - 1)
         {
            targetY = 0;
         }

         ExecuteCommand(new Coordinate2D(targetX, targetY));
      }
   }

   private static (Coordinate2D topLeft, Coordinate2D bottomRight) FindLargestArea(GameData gameData)
   {
      throw new NotImplementedException();
   }

   private static GameData GetGameData(int opponentCount, GameData? lastGameData)
   {
      int gameRound = int.Parse(ReadInput());
      var inputs = ReadInput().Split(' ');

      int x = int.Parse(inputs[0]);
      int y = int.Parse(inputs[1]);
      int backInTimeLeft = int.Parse(inputs[2]);

      Player myPlayer = new(new Coordinate2D(x, y), backInTimeLeft);

      List<Player> opponents = new(opponentCount);
      for (int i = 0; i < opponentCount; i++)
      {
         inputs = ReadInput().Split(' ');
         int opponentX = int.Parse(inputs[0]);
         int opponentY = int.Parse(inputs[1]);
         int opponentBackInTimeLeft = int.Parse(inputs[2]);

         opponents.Add(new Player(new Coordinate2D(opponentX, opponentY), opponentBackInTimeLeft));
      }

      List<IReadOnlyList<int>> grid = new(FieldHeight);

      for (int i = 0; i < FieldHeight; i++)
      {
         var fieldLine = ReadInput().Select(x =>
         {
            if (x == '.')
            {
               return -1;
            }

            return int.Parse(x.ToString());
         }).ToArray();
         grid.Add(fieldLine);
      }

      var wasBackInTime = lastGameData is not null && lastGameData.GameTurn != gameRound;
      return new GameData(myPlayer, gameRound, opponents, new GameField(grid), wasBackInTime);
   }

   private static void GoBackInTime(int roundCount)
   {
      Console.WriteLine($"BACK {roundCount}");
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   #endregion
}

public sealed class Player
{
   #region Constructors and Destructors

   public Player(Coordinate2D position, int backInTimeLeft)
   {
      BackInTimeLeft = backInTimeLeft;
      Position = position ?? throw new ArgumentNullException(nameof(position));
   }

   #endregion

   #region Public Properties

   public int BackInTimeLeft { get; }

   public bool CanGoBack => BackInTimeLeft > 0;

   public bool InPlay => Position.X < 0 || Position.Y < 0;

   public Coordinate2D Position { get; }

   #endregion
}

public sealed class GameData
{
   #region Constructors and Destructors

   public GameData(Player myPlayer, int gameTurn, IReadOnlyList<Player> opponents, GameField gameField, bool wasBackInTime)
   {
      MyPlayer = myPlayer ?? throw new ArgumentNullException(nameof(myPlayer));
      GameTurn = gameTurn;
      Opponents = opponents ?? throw new ArgumentNullException(nameof(opponents));
      GameField = gameField ?? throw new ArgumentNullException(nameof(gameField));
      WasBackInTime = wasBackInTime;
   }

   #endregion

   #region Public Properties

   public GameField GameField { get; }

   public int GameTurn { get; }

   public Player MyPlayer { get; }

   public IReadOnlyList<Player> Opponents { get; }

   public bool WasBackInTime { get; }

   #endregion
}

public sealed class GameField
{
   #region Constants and Fields

   private readonly IReadOnlyList<IReadOnlyList<int>> grid;

   #endregion

   #region Constructors and Destructors

   public GameField(IReadOnlyList<IReadOnlyList<int>> grid)
   {
      this.grid = grid;
   }

   #endregion

   #region Public Properties

   public int LineCount => grid.Count;

   #endregion

   #region Public Indexers

   public IReadOnlyList<int> this[int line] => grid[line];

   #endregion
}

public sealed class Coordinate2D
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