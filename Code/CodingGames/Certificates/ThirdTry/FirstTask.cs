namespace CodingGames.Certificates.ThirdTry;

using System;
using System.Text;

public class FirstTask
{
   public static void Execute()
   {
      throw new NotImplementedException();
   }

  public static void Main()
   {
      string s1 = ReadInput().ToLower();
      string s2 = ReadInput().ToLower();
    
      for (int i = 1; i <= s1.Length; i++)
      {
         var part1 = s1[..i];
         var part2 = s1[i..];
         var rotate = part2 + part1;
         if (rotate==s2)
         {
            Console.WriteLine("true");
            return;
         }
      }

      Console.WriteLine("false");
   }

  private static string? ReadInput()
  {
     var readInput = Console.ReadLine();
     Console.Error.WriteLine(readInput);
     return readInput;
  }
}