namespace CodingGames.Puzzles.Easy;

using System;
using System.Collections.Generic;
using System.Linq;

public class Lumen
{
   #region Public Methods and Operators

   public static void Main()
   {
      int N = int.Parse(ReadInput());
      int L = int.Parse(ReadInput());

      char[][] room = new char[N][];
      for (int i = 0; i < N; i++)
      {
         string[] inputs = ReadInput().Split(' ');

         room[i] = inputs.Select(x => x[0]).ToArray();
      }

      int lightRadius = L - 1;
      for (int i = 0; i < room.Length; i++)
      {
         for (int j = 0; j < room[i].Length; j++)
         {
            var actualCell = room[i][j];
            if (actualCell == 'C')
            {
               Console.Error.WriteLine($"Found candle: {j},{i}");
               SetCandleLight(room, j, i, lightRadius);
            }
         }
      }

      int count = 0;
      for (int i = 0; i < room.Length; i++)
      {
         for (int j = 0; j < room[i].Length; j++)
         {
            var actualCell = room[i][j];
            if (actualCell == 'X')
            {
               count++;
            }
         }
      }

      Console.WriteLine(count);
   }

   #endregion

   #region Methods

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   private static void SetCandleLight(IReadOnlyList<char[]> room, int startX, int startY, int lightSize)
   {
      int i = startY - lightSize;
      if (i < 0)
      {
         i = 0;
      }

      int endI = startY + lightSize;
      if (endI >= room.Count)
      {
         endI = room.Count - 1;
      }

      for (; i <= endI; i++)
      {
         int j = startX - lightSize;
         if (j < 0)
         {
            j = 0;
         }

         int endJ = startX + lightSize;
         if (endJ >= room.Count)
         {
            endJ = room.Count - 1;
         }

         for (; j <= endJ; j++)
         {
            if (room[i][j] == 'C')
            {
               continue;
            }

            room[i][j] = 'L';
            Console.Error.WriteLine($"Set Light: {j},{i}");
         }
      }
   }

   #endregion
}