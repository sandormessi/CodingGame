namespace CodingGames.Events.WinterChallenge2023;

public class SixthTask
{
   #region Public Methods and Operators

   public static int Collisions(List<List<int>> particles)
   {
      int collisions = 0;
      var pointsCount = particles.Count;
      for (var i = 0; i < pointsCount; i++)
      {
         for (var j = i + 1; j < pointsCount; j++)
         {
            //var particle1 = particles[i];
            //var particle2 = particles[j];

            //int x1 = particle1[0];
            //int y1 = particle1[1];
            //int x2 = particle2[0];
            //int y2 = particle2[1];

            //if (Math.Abs(x1 - x2) > 1000 || Math.Abs(y1 - y2) > 1000)
            //{
            //   continue;
            //}

            //var distance = CalculateDistance(x1, y1, x2, y2);
            //if (distance <= 1000)
            //{
            //   collisions++;
            //}
         }
      }

      return collisions;
   }

   #endregion

   #region Methods

   private static double CalculateDistance(int x1, int y1, int x2, int y2)
   {
      var p1XDiff = x1 - x2;
      var squareOfX = Math.Pow(p1XDiff, 2);
      var p1YDiff = y1 - y2;
      var squareOfY = Math.Pow(p1YDiff, 2);

      return Math.Sqrt(squareOfX + squareOfY);
   }

   #endregion
}