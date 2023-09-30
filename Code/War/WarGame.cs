namespace War;

public class WarGame
{
  #region Constants and Fields

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

   #endregion

   #region Methods

   private static List<Card>? Battle(List<Card> cardsOfPlayer1, List<Card> cardsOfPlayer2, Card card1,
      Card card2)
   {
      if (card1.Value > card2.Value)
      {
         return cardsOfPlayer1;
      }

      if (card1.Value < card2.Value)
      {
         return cardsOfPlayer2;
      }

      return null;
   }

   private static IEnumerable<Card> GetCards(IReadOnlyList<Card> cardsOfPlayer, int limit)
   {
      for (var i = 0; i < limit; i++)
      {
         yield return cardsOfPlayer[i];
      }
   }

   private static IEnumerable<Card> GetCardsFromDeck(List<Card> cardsOfPlayer, int cardCount)
   {
      IReadOnlyList<Card> cards = GetCards(cardsOfPlayer, cardCount).ToArray();

      cardsOfPlayer.RemoveRange(0, cardCount);
      return cards;
   }

   private static void Main()
   {
      int cardNumberOfPlayer1 = int.Parse(ReadInput());
      List<Card> cardsOfPlayer1 = new(cardNumberOfPlayer1);
      for (var i = 0; i < cardNumberOfPlayer1; i++)
      {
         string cardString = ReadInput();
         string card = cardString.Remove(cardString.Length - 1, 1);

         cardsOfPlayer1.Add(CardsValueOrder.First(x => x.Equals(card)));
      }

      int cardNumberOfPlayer2 = int.Parse(ReadInput());
      List<Card> cardsOfPlayer2 = new(cardNumberOfPlayer2);
      for (var i = 0; i < cardNumberOfPlayer2; i++)
      {
         string cardString = ReadInput();
         string card = cardString.Remove(cardString.Length - 1, 1);

         cardsOfPlayer2.Add(CardsValueOrder.First(x => x.Equals(card)));
      }

      var roundCount = 0;
      while (cardsOfPlayer1.Any() && cardsOfPlayer2.Any())
      {
         List<Card> takenCardsFromPlayer1 = new(GetCardsFromDeck(cardsOfPlayer1, 1));
         List<Card> takenCardsFromPlayer2 = new(GetCardsFromDeck(cardsOfPlayer2, 1));

         Card cardOfPlayer1 = takenCardsFromPlayer1[0];
         Card cardOfPlayer2 = takenCardsFromPlayer2[0];

         List<Card>? winner = Battle(cardsOfPlayer1, cardsOfPlayer2, cardOfPlayer1, cardOfPlayer2);
         roundCount++;
         if (winner is not null)
         {
            PlaceCardsBottomOfDeck(winner, cardOfPlayer1, cardOfPlayer2);
         }
         else
         {
            if (!War(cardsOfPlayer1, cardsOfPlayer2, takenCardsFromPlayer1, takenCardsFromPlayer2))
            {
               break;
            }
         }

         if (!cardsOfPlayer1.Any())
         {
            WinBattle(2, roundCount);
         }
         else
         {
            if (!cardsOfPlayer2.Any())
            {
               WinBattle(1, roundCount);
            }
         }
      }
   }

   private static void Pat()
   {
      Console.WriteLine("PAT");
   }

   private static void PlaceCardsBottomOfDeck(List<Card> playerCards, params Card[] cards)
   {
      playerCards.AddRange(cards);
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   private static bool War(List<Card> cardsOfPlayer1, List<Card> cardsOfPlayer2, List<Card> takenCardsFromPlayer1,
      List<Card> takenCardsFromPlayer2)
   {
      const int cardCount = 4;

      while ((cardsOfPlayer1.Count >= 1) && (cardsOfPlayer2.Count >= 1))
      {
         Console.Error.WriteLine("War.");

         if (cardsOfPlayer1.Count < cardCount)
         {
            break;
         }

         if (cardsOfPlayer2.Count < cardCount)
         {
            break;
         }

         takenCardsFromPlayer1.AddRange(GetCardsFromDeck(cardsOfPlayer1, cardCount));
         takenCardsFromPlayer2.AddRange(GetCardsFromDeck(cardsOfPlayer2, cardCount));

         Card warBattleCard1 = takenCardsFromPlayer1[^1];
         Card warBattleCard2 = takenCardsFromPlayer2[^1];

         List<Card>? winnerOfWarBattle = Battle(cardsOfPlayer1, cardsOfPlayer2, warBattleCard1, warBattleCard2);
         if (winnerOfWarBattle is not null)
         {
            PlaceCardsBottomOfDeck(winnerOfWarBattle, takenCardsFromPlayer1.Concat(takenCardsFromPlayer2).ToArray());
            return true;
         }
      }

      Pat();
      return false;
   }

   private static void WinBattle(int player, int round)
   {
      Console.WriteLine($"{player} {round}");
   }

   #endregion

   private sealed class Card : IEquatable<string>
   {
      #region Constructors and Destructors

      public Card(string name, int value)
      {
         Name = name ?? throw new ArgumentNullException(nameof(name));
         Value = value;
      }

      #endregion

      #region IEquatable<string> Members

      public bool Equals(string? other)
      {
         if (other is null)
         {
            return false;
         }

         return Name.Equals(other, StringComparison.CurrentCultureIgnoreCase);
      }

      #endregion

      #region Public Properties

      public string Name { get; }

      public int Value { get; }

      #endregion

      #region Public Methods and Operators

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

      #endregion

      #region Methods

      private bool Equals(Card? other)
      {
         if (other is null)
         {
            return false;
         }

         return Equals(other.Name);
      }

      #endregion
   }
}
