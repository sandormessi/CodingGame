namespace DeathFirstSearchEpisode1;

public class DeathFirstSearchEpisode1Game
{
   #region Methods

   private static (int linkId1, int linkId2) CalculateLinkToRemove(IEnumerable<Node> nodes, int agentNodeId)
   {
      var agentNode = nodes.First(x => x.Id == agentNodeId);

      IReadOnlyList<Node> actualLevel = new[] { agentNode };

      IEnumerable<Node> nodesVisited = actualLevel;

      List<IEnumerable<Node>> levels = new() { actualLevel };

      var levelCount = 0;
      while (actualLevel.Any())
      {
         foreach (var node in actualLevel)
         {
            Console.Error.Write(node.Id + " ");
         }

         Console.Error.WriteLine();

         var gatewayNode = actualLevel.FirstOrDefault(x => x.IsGateway);
         if (gatewayNode != null)
         {
            var link = FindLink(gatewayNode, levels[levelCount - 1]);
            return (gatewayNode.Id, link.Id);
         }

         actualLevel = actualLevel.SelectMany(x => x.Links).Except(nodesVisited).ToArray();
         levels.Add(actualLevel);
         nodesVisited = nodesVisited.Concat(actualLevel).ToArray();

         levelCount++;
      }

      throw new InvalidOperationException("There is no solution.");
   }

   private static void CutLink(IReadOnlyList<Node> nodes, int linkId1, int linkId2)
   {
      var foundNode1 = nodes.First(x => x.Id == linkId1);
      var foundNode2 = nodes.First(x => x.Id == linkId2);

      var removed = foundNode1.Links.Remove(foundNode2);
      var removed2 = foundNode2.Links.Remove(foundNode1);

      if (!removed || !removed2)
      {
         throw new InvalidOperationException($"There is no link between nodes '{linkId1}' and '{linkId2}'.");
      }

      Console.WriteLine($"{linkId1} {linkId2}");
   }

   private static Node FindLink(Node gatewayNode, IEnumerable<Node> level)
   {
      return gatewayNode.Links.First(level.Contains);
   }

   static void Main()
   {
      var inputs = ReadInput().Split(' ');

      var nodeCount = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
      var linkCount = int.Parse(inputs[1]); // the number of links
      var gatewayCount = int.Parse(inputs[2]); // the number of exit gateways

      List<Node> nodes = new(nodeCount);
      for (var i = 0; i < nodeCount; i++)
      {
         nodes.Add(new Node(i));
      }

      for (var i = 0; i < linkCount; i++)
      {
         inputs = ReadInput().Split(' ');

         var node1 = int.Parse(inputs[0]);
         var node2 = int.Parse(inputs[1]);

         var foundNode1 = nodes.First(x => x.Id == node1);
         var foundNode2 = nodes.First(x => x.Id == node2);

         foundNode1.Links.Add(foundNode2);
         foundNode2.Links.Add(foundNode1);
      }

      for (var i = 0; i < gatewayCount; i++)
      {
         var gatewayNodeIndex = int.Parse(ReadInput()); // the index of a gateway node

         var foundGatewayNode = nodes.First(x => x.Id == gatewayNodeIndex);
         foundGatewayNode.IsGateway = true;
      }

      while (true)
      {
         var agentNodeId = int.Parse(ReadInput());
         var (linkId1, linkId2) = CalculateLinkToRemove(nodes, agentNodeId);
         CutLink(nodes, linkId1, linkId2);
      }
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
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

      public bool IsGateway { get; set; }

      public IList<Node> Links { get; } = new List<Node>();

      #endregion
   }
}