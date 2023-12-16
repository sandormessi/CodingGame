namespace CodingGames.Puzzles.Hard;

using System;
using System.Collections.Generic;
using System.Linq;

public class DeathFirstSearchEpisode2
{
   #region Methods

   private static bool AnalyzeLevel(IReadOnlyList<Node> nodes, IReadOnlyList<Node> actualLevel, int levelCount,
      IReadOnlyList<IReadOnlyList<Node>> levels, out NodePair? linkToRemove)
   {
      linkToRemove = null;
      var gatewayNode = actualLevel.FirstOrDefault(x => x.IsGateway);

      if (gatewayNode == null)
      {
         return false;
      }

      if (levelCount == 1)
      {
         // If gateway is the next node than the closest link must be removed
         var link = FindLink(gatewayNode, levels[levelCount - 1]);
         {
            linkToRemove = new NodePair(gatewayNode, link);
            return true;
         }
      }

      // If there are steps ahead before reach the gateway we must destroy the gateways that is connected with one node: gw-n-gw
      var nodePair = FindClosestSpecialGateway(nodes, levels);
      if (nodePair is null)
      {
         var link = FindLink(gatewayNode, levels[levelCount - 1]);

         linkToRemove = new NodePair(gatewayNode, link);
         return true;
      }

      linkToRemove = new NodePair(nodePair.Node1, nodePair.Node2);
      return true;
   }

   private static NodePair CalculateLinkToRemove(IReadOnlyList<Node> nodes, int agentNodeId)
   {
      var levels = GetLevels(nodes, agentNodeId, Enumerable.Empty<Node>());

      var levelCount = 0;
      foreach (var level in levels)
      {
         if (AnalyzeLevel(nodes, level, levelCount, levels, out var linkToRemove))
         {
            return linkToRemove ?? throw new InvalidOperationException("There is no node to remove.");
         }

         levelCount++;
      }

      throw new InvalidOperationException("There is no solution.");
   }

   private static IReadOnlyList<Node> CollectDangerousLinks(Node closestGatewayNode)
   {
      List<Node> dangerousLinks = new(closestGatewayNode.Links.Count);

      foreach (var link in closestGatewayNode.Links)
      {
         var isDangerous = link.Links.Any(x => x.IsGateway && x.Id != closestGatewayNode.Id);
         if (isDangerous)
         {
            dangerousLinks.Add(link);
         }
      }

      return dangerousLinks;
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

   private static Node? FindClosestGatewayNode(IEnumerable<IReadOnlyList<Node>> levels)
   {
      foreach (var actualLevel in levels)
      {
         var gatewaysOnLevel = actualLevel.FirstOrDefault(x => x.IsGateway);
         if (gatewaysOnLevel is null)
         {
            continue;
         }

         return gatewaysOnLevel;
      }

      return null;
   }

   private static NodePair? FindClosestSpecialGateway(IReadOnlyList<Node> nodes, IReadOnlyList<IReadOnlyList<Node>> levels)
   {
      var gatewayNodeCount = levels.SelectMany(x => x).Count(x => x.IsGateway);

      var closestGatewayNode = FindClosestGatewayNode(levels) ?? throw new InvalidOperationException("There is no closest gateway node.");

      List<Node> nodesOrderedByDistance = new() { closestGatewayNode };
      for (var i = 1; i < gatewayNodeCount; i++)
      {
         var dangerousLinks = CollectDangerousLinks(closestGatewayNode);
         if (!dangerousLinks.Any() /*|| nodesOrderedByDistance.Count == 1*/)
         {
            Console.Error.WriteLine($"Dangerous Links count: {dangerousLinks.Count}, Gateway list count {nodesOrderedByDistance.Count}.");
            Console.Error.WriteLine($"Nearest gateway: {closestGatewayNode.Id}.");

            levels = GetLevels(nodes, closestGatewayNode.Id, nodesOrderedByDistance).Skip(1).ToArray();

            closestGatewayNode = FindClosestGatewayNode(levels) ?? throw new InvalidOperationException("There is no closest gateway node.");

            nodesOrderedByDistance.Add(closestGatewayNode);
         }
         else
         {
            Console.Error.WriteLine("-----------------------------------------------------");
            foreach (var dangerousLink in dangerousLinks)
            {
               Console.Error.WriteLine($"{closestGatewayNode.Id}, {dangerousLink.Id}");
            }

            return SelectClosestDangerousNode(nodesOrderedByDistance, dangerousLinks);
         }
      }

      return null;
   }

   private static Node FindLink(Node gatewayNode, IEnumerable<Node> level)
   {
      return gatewayNode.Links.First(level.Contains);
   }

   private static IReadOnlyList<IReadOnlyList<Node>> GetLevels(IReadOnlyList<Node> nodes, int agentNodeId, IEnumerable<Node> exclude)
   {
      var agentNode = nodes.First(x => x.Id == agentNodeId);

      IReadOnlyList<Node> actualLevel = new[] { agentNode };

      IEnumerable<Node> nodesVisited = actualLevel.Union(exclude).ToArray();

      List<IReadOnlyList<Node>> levels = new() { actualLevel };

      while (actualLevel.Any())
      {
         actualLevel = actualLevel.SelectMany(x => x.Links).Except(nodesVisited).ToArray();
         levels.Add(actualLevel);
         nodesVisited = nodesVisited.Union(actualLevel).ToArray();
      }

      return levels;
   }

   private static void Main()
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
         var nodePair = CalculateLinkToRemove(nodes, agentNodeId);

         CutLink(nodes, nodePair.Node1.Id, nodePair.Node2.Id);
      }
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   private static NodePair SelectClosestDangerousNode(List<Node> nodesOrderedByDistance, IReadOnlyList<Node> dangerousLinks)
   {
      var closestNode = nodesOrderedByDistance.Last();
      return new NodePair(closestNode, dangerousLinks.Last());
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

   private class NodePair
   {
      #region Constructors and Destructors

      public NodePair(Node node1, Node node2)
      {
         Node1 = node1 ?? throw new ArgumentNullException(nameof(node1));
         Node2 = node2 ?? throw new ArgumentNullException(nameof(node2));
      }

      #endregion

      #region Public Properties

      public Node Node1 { get; }

      public Node Node2 { get; }

      #endregion
   }
}