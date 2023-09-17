namespace WinterChallenge2023;

internal class SeventhTask
{
   private sealed class Node
   {
      public Node(int id)
      {
         Id = id;
      }

      public int Id { get; }

      public IList<Node> Neighbors { get; } = new List<Node>();
   }

   public static int CountStrands(List<int> atoms, List<List<int>> bonds)
   {
      IReadOnlyList< Node> nodes = atoms.Select(x => new Node(x)).ToArray();

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
}