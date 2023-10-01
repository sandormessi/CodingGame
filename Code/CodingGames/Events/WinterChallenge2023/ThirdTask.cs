namespace CodingGames.Events.WinterChallenge2023;

internal class ThirdTask
{
   #region Public Methods and Operators

   public static List<List<int>> Solve(List<List<int>> grid, List<Unknown> rules)
   {
      List<List<int>> solutionGrid = new List<List<int>>();

      for (int i = 0; i < grid.Count; i++)
      {
         for (int j = 0; j < grid[i].Count; j++)
         {
         }
      }

      return new List<List<int>>() { new List<int>() { 52, 116, 10 }, new List<int>() { 24, 47, 35 }, new List<int>() { 156, 54, 145 } };
   }

   #endregion

   public class Unknown
   {
      #region Public Properties

      public List<int> Pattern { get; set; }

      public int Result { get; set; }

      #endregion
   }
}