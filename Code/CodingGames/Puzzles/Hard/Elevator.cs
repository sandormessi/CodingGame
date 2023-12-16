namespace CodingGames.Puzzles.Hard;

using System;
using System.Collections.Generic;

public class Elevator
{
   #region Public Methods and Operators

   public static void Main()
   {
      string[] inputs = ReadInput().Split(' ');

      int buildingHeight = int.Parse(inputs[0]);
      int floorsUp = int.Parse(inputs[1]);
      int floorsDown = int.Parse(inputs[2]);
      int actualFloor = int.Parse(inputs[3]);
      int targetFloor = int.Parse(inputs[4]);

      var count = 0;

      DebugMessage($"Actual floor: {actualFloor}");
      DebugMessage($"Target floor: {targetFloor}");
      DebugMessage($"Floors Up: {floorsUp}");
      DebugMessage($"Floors Down: {floorsDown}");
      DebugMessage($"Building height: {buildingHeight}");

      List<int> floorsSoFar = new() { actualFloor };

      while (actualFloor != targetFloor)
      {
         int newFloor;
         if (targetFloor > actualFloor)
         {
            newFloor = GoUp(actualFloor, floorsUp, buildingHeight);

            if (actualFloor == newFloor)
            {
               newFloor = GoDown(actualFloor, floorsDown);
            }

            DebugMessage($"Go up: actual floor: {actualFloor}");
         }
         else if (targetFloor < actualFloor)
         {
            newFloor = GoDown(actualFloor, floorsDown);

            if (actualFloor == newFloor)
            {
               newFloor = GoUp(actualFloor, floorsUp, buildingHeight);
            }

            DebugMessage($"Go down: actual floor: {actualFloor}");
         }
         else
         {
            throw new InvalidOperationException("Invalid algorithm.");
         }

         actualFloor = newFloor;

         if (floorsSoFar.Contains(actualFloor))
         {
            Console.WriteLine("IMPOSSIBLE");
            return;
         }

         floorsSoFar.Add(actualFloor);

         count++;
      }

      if (count < 0)
      {
         Console.WriteLine("IMPOSSIBLE");
      }
      else
      {
         Console.WriteLine(count);
      }
   }

   #endregion

   #region Methods

   private static void DebugMessage<T>(T message)
   {
      Console.Error.WriteLine(message);
   }

   private static int GoDown(int actualFloor, int floorsDown)
   {
      var newFloor = actualFloor - floorsDown;
      if (newFloor < 1)
      {
         return actualFloor;
      }

      return newFloor;
   }

   private static int GoUp(int actualFloor, int floorsUp, int buildingHeight)
   {
      var newFloor = actualFloor + floorsUp;
      if (newFloor > buildingHeight)
      {
         return actualFloor;
      }

      return newFloor;
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   #endregion
}