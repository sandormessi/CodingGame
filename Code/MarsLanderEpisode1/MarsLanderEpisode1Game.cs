namespace MarsLanderEpisode1;

using System.Drawing;

public class MarsLanderEpisode1Game
{
   #region Methods

   private static void ChangePower(ref int power, int offset)
   {
      var newPower = power + offset;
      if (newPower is >= 0 and <= 4)
      {
         power = newPower;
      }
   }

   static void Main()
   {
      string[] inputs;
      int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.

      List<Point> surfacePoints = new(surfaceN);
      for (int i = 0; i < surfaceN; i++)
      {
         inputs = Console.ReadLine().Split(' ');
         int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
         int
            landY = int.Parse(
               inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
         surfacePoints.Add(new Point(landX, landY));
      }

      Console.Error.WriteLine(surfaceN);
      // game loop
      while (true)
      {
         inputs = Console.ReadLine().Split(' ');
         int X = int.Parse(inputs[0]);
         int Y = int.Parse(inputs[1]);
         int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
         int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
         int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
         int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
         int power = int.Parse(inputs[6]); // the thrust power (0 to 4).

         var speed = Math.Abs(vSpeed);
         if (speed == 0)
         {
            ChangePower(ref power, -1);
         }
         else
         {
            if (speed >= 40)
            {
               ChangePower(ref power, 1);
            }
            else
            {
               ChangePower(ref power, -1);
            }
         }

         Console.WriteLine($"0 {power}");
      }
   }

   #endregion
}