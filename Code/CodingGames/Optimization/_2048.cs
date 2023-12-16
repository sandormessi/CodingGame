namespace CodingGames.Optimization;

using System;
using System.Collections.Generic;
using System.Linq;

public class _2048
{
   #region Constants and Fields

   private const int BoardSize = 4;

   #endregion

   #region Enums

   private enum Commands
   {
      Left,

      Right,

      Up,

      Down
   }

   #endregion

   #region Public Methods and Operators

   public static void Main()
   {
      Commands[] allCommands = { Commands.Left, Commands.Up, Commands.Right, Commands.Down };
      int[][] board = new int[BoardSize][];

      while (true)
      {
         var seed = int.Parse(ReadInput());
         var score = int.Parse(ReadInput());

         for (var i = 0; i < BoardSize; i++)
         {
            board[i] = new int[BoardSize];
            string[] inputs = ReadInput().Split(' ');
            for (var j = 0; j < BoardSize; j++)
            {
               int cell = int.Parse(inputs[j]);
               board[i][j] = cell;
            }
         }

         for (int i = 0; i < board.Length; i++)
         {
            for (int j = 0; j < board[i].Length; j++)
            {
               Console.Error.Write(board[i][j]);
            }

            Console.Error.WriteLine();
         }

         var calculateScores = allCommands.Select(x => new KeyValuePair<int, Commands>(CalculateScore(board, x), x)).OrderByDescending(x => x.Key)
            .ToArray();

         var bestCommand = Commands.Left;

         foreach (var calculateScore in calculateScores)
         {
            Console.Error.WriteLine($"Command {calculateScore.Value}, Value {calculateScore.Key}");
         }

         foreach (var scores in calculateScores)
         {
            bestCommand = scores.Value;
            if (scores.Key > 0)
            {
               break;
            }

            bool canMove = CanMove(board, bestCommand);

            if (canMove)
            {
               break;
            }
         }

         ExecuteCommand(bestCommand);
      }
   }

   #endregion

   #region Methods

   private static int CalculateScore(IReadOnlyList<int[]> board, Commands command)
   {
      int score = 0;
      switch (command)
      {
         case Commands.Left:

            foreach (var cellsInLine in board)
            {
               var lineWithoutZeros = cellsInLine.Where(x => x != 0).ToList();

               for (var j = 1; j < lineWithoutZeros.Count; j++)
               {
                  var previousCell = lineWithoutZeros[j - 1];
                  if (lineWithoutZeros[j] == previousCell)
                  {
                     score += lineWithoutZeros[j] + previousCell;
                     j++;
                  }
               }
            }

            break;
         case Commands.Right:

            foreach (var cellsInLine in board)
            {
               var lineWithoutZeros = cellsInLine.Where(x => x != 0).ToList();

               for (var j = lineWithoutZeros.Count - 2; j >= 0; j--)
               {
                  var previousCell = lineWithoutZeros[j + 1];
                  if (lineWithoutZeros[j] == previousCell)
                  {
                     score += lineWithoutZeros[j] + previousCell;
                     j--;
                  }
               }
            }

            break;
         case Commands.Up:

            for (var index = 0; index < board.Count; index++)
            {
               List<int> column = new(BoardSize);
               for (var i = 0; i < BoardSize; i++)
               {
                  var cell = board[i][index];
                  if (cell != 0)
                  {
                     column.Add(cell);
                  }
               }

               for (var i = 1; i < column.Count; i++)
               {
                  var previousCell = column[i - 1];
                  if (column[i] == previousCell)
                  {
                     score += column[i] + previousCell;
                     i++;
                  }
               }
            }

            break;
         case Commands.Down:

            for (var index = 0; index < board.Count; index++)
            {
               List<int> column = new(BoardSize);
               for (var i = 0; i < BoardSize; i++)
               {
                  var cell = board[i][index];
                  if (cell != 0)
                  {
                     column.Add(cell);
                  }
               }

               for (var i = column.Count - 2; i >= 0; i--)
               {
                  var previousCell = column[i + 1];
                  if (column[i] == previousCell)
                  {
                     score += column[i] + previousCell;
                     i--;
                  }
               }
            }

            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(command), command, null);
      }

      return score;
   }

   private static bool CanMove(int[][] board, Commands command)
   {
      switch (command)
      {
         case Commands.Left:

            foreach (var line in board)
            {
               var numbersAfterZero = line.SkipWhile(x => x != 0).SkipWhile(x => x == 0);
               if (numbersAfterZero.Any())
               {
                  return true;
               }
            }

            return false;

         case Commands.Right:

            foreach (var line in board)
            {
               var numbersAfterZero = line.Reverse().SkipWhile(x => x != 0).SkipWhile(x => x == 0);
               if (numbersAfterZero.Any())
               {
                  return true;
               }
            }

            return false;

         case Commands.Up:

            for (var index = 0; index < board.Length; index++)
            {
               List<int> column = new(BoardSize);
               for (var i = 0; i < BoardSize; i++)
               {
                  var cell = board[i][index];
                  if (cell != 0)
                  {
                     column.Add(cell);
                  }
               }

               var numbersAfterZero = column.SkipWhile(x => x != 0).SkipWhile(x => x == 0);
               if (numbersAfterZero.Any())
               {
                  return true;
               }
            }

            return false;

         case Commands.Down:

            for (var index = 0; index < board.Length; index++)
            {
               IList<int> column = new List<int>(BoardSize);
               for (var i = 0; i < BoardSize; i++)
               {
                  var cell = board[i][index];
                  if (cell != 0)
                  {
                     column.Add(cell);
                  }
               }

               var numbersAfterZero = column.Reverse().SkipWhile(x => x != 0).SkipWhile(x => x == 0);
               if (numbersAfterZero.Any())
               {
                  return true;
               }
            }

            return false;

         default:
            throw new ArgumentOutOfRangeException(nameof(command), command, null);
      }
   }

   private static void ExecuteCommand(Commands command)
   {
      switch (command)
      {
         case Commands.Left:

            Console.WriteLine("L");

            break;
         case Commands.Right:

            Console.WriteLine("R");

            break;
         case Commands.Up:

            Console.WriteLine("U");

            break;
         case Commands.Down:

            Console.WriteLine("D");

            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(command), command, null);
      }
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   #endregion
}