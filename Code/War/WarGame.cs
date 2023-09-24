namespace War;

public class WarGame
{
   private class Card:IEquatable<string>
   {
      public Card(string name, int value)
      {
         Name = name ?? throw new ArgumentNullException(nameof(name));
         Value = value;
      }

      public string Name { get; }
      public int Value { get; }
      private bool Equals(Card? other)
      {
         if (other is null)
         {
            return false;
         }

         return Equals(other.Name);
      }

      public override bool Equals(object? obj)
      {
         if (obj is null)
         {
            return false;
         }

         if (ReferenceEquals(this, obj))
         {
            return true;
         }

         if (obj.GetType() != this.GetType())
         {
            return false;
         }

         return Equals((Card)obj);
      }

      public override int GetHashCode()
      {
         return Name.GetHashCode();
      }

      public bool Equals(string? other)
      {
         if (other is null)
         {
            return false;
         }

         return Name.Equals(other, StringComparison.CurrentCultureIgnoreCase);
      }
   }

   private static readonly IReadOnlyList<Card> CardsValueOrder = new List<Card>
   {
      new("2", 1),
      new("3", 2),
      new("4", 3),
      new("5", 4),
      new("6", 5),
      new("7", 6),
      new("8", 7),
      new("9", 8),
      new("10", 9),
      new("J", 10),
      new("Q", 11),
      new("K", 12),
      new("A", 13),
   };

   private static void Main(string[] args)
   {
      var cardNumberOfPlayer1 = int.Parse(ReadInput());
      List<Card> cardsOfPlayer1 = new(cardNumberOfPlayer1);
      for (var i = 0; i < cardNumberOfPlayer1; i++)
      {
         var cardString = ReadInput();
         var card = cardString.Remove(cardString.Length - 1, 1);

         cardsOfPlayer1.Add(CardsValueOrder.First(x => x.Equals(card)));
      }

      var cardNumberOfPlayer2 = int.Parse(ReadInput());
      List<Card> cardsOfPlayer2 = new(cardNumberOfPlayer2);
      for (var i = 0; i < cardNumberOfPlayer2; i++)
      {
         var cardString = ReadInput();
         var card = cardString.Remove(cardString.Length - 1, 1);

         cardsOfPlayer2.Add(CardsValueOrder.First(x => x.Equals(card)));
      }

      var roundCount = 1;
      while (cardsOfPlayer1.Any() && cardsOfPlayer2.Any())
      {
         IEnumerable<Card> takenCardsFromPlayer1 = GetCardsFromDeck(cardsOfPlayer1, 1);
         IEnumerable<Card> takenCardsFromPlayer2 = GetCardsFromDeck(cardsOfPlayer2, 1);

         var cardOfPlayer1 = takenCardsFromPlayer1.First();
         var cardOfPlayer2 = takenCardsFromPlayer2.First();

         if (Battle(cardsOfPlayer1, cardsOfPlayer2, cardOfPlayer1, cardOfPlayer2, roundCount))
         {
            continue;
         }

         const int cardCount = 3;

         IReadOnlyList<Card> taken3CardsFromPlayer1 = GetCardsFromDeck(cardsOfPlayer1, cardCount).ToArray();
         IReadOnlyList<Card> taken3CardsFromPlayer2 = GetCardsFromDeck(cardsOfPlayer2, cardCount).ToArray();

         for (var i = 0; i < cardCount; i++)
         {
            var card1 = taken3CardsFromPlayer1[i];
            var card2 = taken3CardsFromPlayer2[i];

            if (Battle(cardsOfPlayer1, cardsOfPlayer2, card1, card2, roundCount))
            {
               break;
            }
         }

         if (cardsOfPlayer1.Any())
         {
         }

         roundCount++;
      }
   }

   private static bool Battle(IList<Card> cardsOfPlayer1, IList<Card> cardsOfPlayer2, Card card1, Card card2, int round)
   {
      if (card1.Value > card2.Value)
      {
         PlaceCardsBottomOfDeck(cardsOfPlayer1, card1, card2);
         return true;
      }

      if (card1.Value < card2.Value)
      {
         PlaceCardsBottomOfDeck(cardsOfPlayer2, card1, card2);
         return true;
      }

      return false;
   }

   private static void WinBattle(int player, int round)
   {
      Console.WriteLine($"{player} {round}");
   }
   private static IEnumerable<Card> GetCardsFromDeck(List<Card> cardsOfPlayer, int cardCount)
   {
      var limit = cardsOfPlayer.Count - cardCount;
      for (var i = cardsOfPlayer.Count - 1; i >= limit; i--)
      {
         yield return cardsOfPlayer[i];
      }

      cardsOfPlayer.RemoveRange(limit, cardCount);
   }

   private static void PlaceCardsBottomOfDeck(IList<Card> playerCards, params Card[] cards)
   {
      foreach (var card in cards)
      {
         playerCards.Insert(0, card);
      }
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }
}