namespace CodingGames.Puzzles.Medium;

using System.Diagnostics;

public class TelephoneNumbers
{
   #region Public Methods and Operators

   public static void Main()
   {
      int numberOfTelephoneNumbers = int.Parse(ReadInput());

      List<TelephoneNumber> telephoneNumbers = new();
      for (int i = 0; i < numberOfTelephoneNumbers; i++)
      {
         string telephone = ReadInput();
         telephoneNumbers.Add(new TelephoneNumber(GetDigits(telephone)));
      }

      TelephoneNumber longestNumber = telephoneNumbers.MaxBy(x => x.Digits.Count)!;

      IReadOnlyList<Node> firstLevel = telephoneNumbers.Select(x => x.Digits[0]).Distinct().Select(x => new Node(x, 0)).ToArray();
      IReadOnlyList<Node> nodes = firstLevel;

      for (int i = 1; i < longestNumber.Digits.Count; i++)
      {
         var relevantNumbers = telephoneNumbers.Where(x => x.Digits.Count > i);

         foreach (var number in relevantNumbers)
         {
            var actualDigit = number.Digits[i];
            Node foundNode = FindNode(firstLevel, number, i);
            var foundChildren = foundNode.Children.FirstOrDefault(x => x.Id == actualDigit);
            if (foundChildren is null)
            {
               var node = new Node(actualDigit, i) { Parent = foundNode };

               foundNode.Children.Add(node);
            }
         }

         nodes = nodes.SelectMany(x => x.Children).ToArray();
      }

      int count = CountOfNodes(firstLevel);
      Console.WriteLine(count);
   }

   #endregion

   #region Methods

   private static int CountOfNodes(IReadOnlyList<Node> firstLevel)
   {
      int count = 0;
      var actualLevel = firstLevel;
      while (actualLevel.Any())
      {
         count += actualLevel.Count;
         actualLevel = actualLevel.SelectMany(x => x.Children).ToArray();
      }

      return count;
   }

   private static Node FindNode(IReadOnlyList<Node> firstLevel, TelephoneNumber telephoneNumber, int i)
   {
      var foundNode = firstLevel.First(x => x.Id == telephoneNumber.Digits[0]);
      for (int j = 1; j < i; j++)
      {
         foundNode = foundNode.Children.First(x => x.Id == telephoneNumber.Digits[j]);
      }

      return foundNode;
   }

   private static IEnumerable<int> GetDigits(string number)
   {
      return number.Select(x => int.Parse(x.ToString()));
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   #endregion

   [DebuggerDisplay("{Id},Parent:{Parent}")]
   private sealed class Node
   {
      #region Constants and Fields

      public readonly List<Node> Children = new();

      public readonly List<TelephoneNumber> TelephoneNumbers = new();

      #endregion

      #region Constructors and Destructors

      public Node(int id, int level)
      {
         Id = id;
         Level = level;
      }

      #endregion

      #region Public Properties

      public int Id { get; }

      public int Level { get; }

      public Node? Parent { get; set; }

      #endregion
   }

   private sealed class TelephoneNumber
   {
      #region Constants and Fields

      public readonly IReadOnlyList<int> Digits;

      #endregion

      #region Constructors and Destructors

      public TelephoneNumber(IEnumerable<int> digits)
      {
         Digits = digits.ToArray();
      }

      #endregion
   }
}