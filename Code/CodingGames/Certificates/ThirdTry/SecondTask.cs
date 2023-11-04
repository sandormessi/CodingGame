namespace CodingGames.Certificates.ThirdTry;

public class SecondTask
{
   public static void Main()
   {
      int N = int.Parse(Console.ReadLine());
      var firstANdLastLine = string.Join(string.Empty, Enumerable.Repeat("#", N));
      if (N == 1)
      {
         Console.WriteLine("#");
         return;
      }

      Console.WriteLine(firstANdLastLine);

      var immediateLine = string.Join(string.Empty, Enumerable.Repeat(" ", N - 2));
      for (int i = 1; i < N - 1; i++)
      {
         Console.WriteLine($"#{immediateLine}#");
      }

      Console.WriteLine(firstANdLastLine);
   }
}