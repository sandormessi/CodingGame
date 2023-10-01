namespace CodingGames.Events.WinterChallenge2023;

internal class SeventhTask
{
   #region Public Methods and Operators

   public static int CountStrands(List<int> atoms, List<List<int>> bonds)
   {
      IReadOnlyList<Node> nodes = atoms.Select(x => new Node(x)).ToArray();

      List<List<int>> paths = new();

      foreach (var bond in bonds)
      {
         var nodeId = bond[0];
         var foundNode = nodes.First(x => x.Id == nodeId);

         var foundNeighbor = nodes.FirstOrDefault(x => x.Id == bond[1]);
         foundNode.Neighbors.Add(foundNeighbor);
      }

      foreach (var node in nodes)
      {
      }

      return 15;
   }

   #endregion

   private sealed class Node
   {
      #region Constructors and Destructors

      public Node(int id)
      {
         Id = id;
      }

      #endregion

      #region Public Properties

      public int Id { get; }

      public IList<Node> Neighbors { get; } = new List<Node>();

      #endregion
   }
}