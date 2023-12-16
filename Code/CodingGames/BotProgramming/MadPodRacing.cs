namespace CodingGames.BotProgramming;

using System;

public class MadPodRacing
{
   public static void Main()
   {
      int lastDistance = int.MaxValue;
      while (true)
      {
         string[] inputs = Console.ReadLine().Split(' ');
         int x = int.Parse(inputs[0]);
         int y = int.Parse(inputs[1]);
         int nextCheckpointX = int.Parse(inputs[2]); // x position of the next check point
         int nextCheckpointY = int.Parse(inputs[3]); // y position of the next check point
         int nextCheckpointDist = int.Parse(inputs[4]); // distance to the next checkpoint
         int nextCheckpointAngle = int.Parse(inputs[5]); // angle between your pod orientation and the direction of the next checkpoint
         inputs = Console.ReadLine().Split(' ');
         int opponentX = int.Parse(inputs[0]);
         int opponentY = int.Parse(inputs[1]);
         
         DebugMessage($"Distance: {nextCheckpointDist}");
         DebugMessage($"Angle: {nextCheckpointAngle}");

         if (nextCheckpointDist > lastDistance)
         {
            Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " 100");
         }
         else if (nextCheckpointAngle is > 90 or < -90 || nextCheckpointDist < 2000)
         {
            Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " 0");
         }
         else
         {
            Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " 100");
         }

         lastDistance = nextCheckpointDist;
      }
   }

   private static void DebugMessage(string message)
   {
      Console.Error.WriteLine(message);
   }
}