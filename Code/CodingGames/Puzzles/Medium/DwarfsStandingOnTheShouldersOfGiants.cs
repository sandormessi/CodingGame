namespace CodingGames.Puzzles.Medium;

public class DwarfsStandingOnTheShouldersOfGiants
{
   #region Public Methods and Operators

   public static void Main()
   {
      int n = int.Parse(ReadInput());

      List<Node> nodes = new(n);
      for (int i = 0; i < n; i++)
      {
         string[] inputs = ReadInput().Split(' ');

         int node1Id = int.Parse(inputs[0]);
         int node2Id = int.Parse(inputs[1]);

         var foundNode1 = nodes.FirstOrDefault(x => x.Id == node1Id);
         if (foundNode1 is null)
         {
            foundNode1 = new Node(node1Id);
            nodes.Add(foundNode1);
         }

         var foundNode2 = nodes.FirstOrDefault(x => x.Id == node2Id);
         if (foundNode2 is null)
         {
            foundNode2 = new Node(node2Id) { Parent = foundNode1 };
            nodes.Add(foundNode2);
         }

         foundNode1.OutGoingNodes.Add(foundNode2);
      }

      List<int> nodeCounts = new();
      foreach (var node in nodes)
      {
         var nodeCount = FindNodes(node);
         nodeCounts.Add(nodeCount);
      }

      Console.WriteLine(nodeCounts.Max());
   }

   #endregion

   #region Methods

   private static int FindNodes(Node node)
   {
      IReadOnlyList<Node> actualLevel = new[] { node };
      int count = 0;
      while (actualLevel.Any())
      {
         actualLevel = actualLevel.SelectMany(x => x.OutGoingNodes).DistinctBy(x => x.Id).ToArray();

         count++;
      }

      return count;
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   #endregion

   private class Node
   {
      #region Constructors and Destructors

      public Node(int id)
      {
         Id = id;
      }

      #endregion

      #region Public Properties

      public int Id { get; }

      public IList<Node> OutGoingNodes { get; } = new List<Node>();

      public Node Parent { get; set; }

      #endregion
   }
}