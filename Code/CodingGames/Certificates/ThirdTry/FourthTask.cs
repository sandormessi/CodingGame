namespace CodingGames.Certificates.ThirdTry;

public class FourthTask
{
 
   private sealed class Chlothes:IEquatable<Chlothes>
   {
      public Chlothes(string type, int size, string color)
      {
         Type = type;
         Size = size;
         Color = color;
      }

      public string Type { get; }
      public int Size { get; }
      public string Color { get; }

      public bool Equals(Chlothes? other)
      {
         if (ReferenceEquals(null, other))
            return false;
         if (ReferenceEquals(this, other))
            return true;
         return Type == other.Type && Size == other.Size && Color == other.Color;
      }
      public override bool Equals(object? obj)
      {
         return ReferenceEquals(this, obj) || obj is Chlothes other && Equals(other);
      }

      public override int GetHashCode()
      {
         return HashCode.Combine(Type, Size, Color);
      }
   }
   public static void Main()
   {
      int n = int.Parse(Console.ReadLine());

      List<Chlothes> chlothesList = new(n);
      for (int i = 0; i < n; i++)
      {
         string[] inputs = Console.ReadLine().Split(' ');
         string clothesType = inputs[0].ToLower();
         int clothesSize = int.Parse(inputs[1]);
         string clothesColor = inputs[2].ToLower();

         chlothesList.Add(new Chlothes(clothesType, clothesSize, clothesColor));
      }

      List<Chlothes> missingChlothesList = new(n);
      foreach (var chlothes in chlothesList)
      {
         if (chlothes.Type != "sock")
         {
            continue;
         }

         var countOfSock = chlothesList.Where(x => x.Equals(chlothes)).ToArray();
         if (countOfSock.Length % 2 == 1)
         {
            if (!missingChlothesList.Contains(chlothes))
            {
               missingChlothesList.Add(chlothes);
            }
         }
      }

      missingChlothesList = missingChlothesList.OrderBy(x => x.Size).ThenBy(x => x.Color).ToList();
      Console.WriteLine(missingChlothesList.Count);
      foreach (var missingChlotes in missingChlothesList)
      {
         Console.WriteLine($"{missingChlotes.Size} {missingChlotes.Color}");
      }
   }
}