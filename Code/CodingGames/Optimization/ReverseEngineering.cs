namespace CodingGames.Optimization;

using System;

public class ReverseEngineering
{
   private static char[] commands = { 'A', 'B', 'C', 'D', 'E' };
   public static void Main()
   {
      int firstInitInput = int.Parse(ReadInput());
      int secondInitInput = int.Parse(ReadInput());
      int thirdInitInput = int.Parse(ReadInput());
     
      int c = 0;
      while (true)
      {
         string firstInput = ReadInput();
         string secondInput = ReadInput();
         string thirdInput = ReadInput();
         string fourthInput = ReadInput();

         for (int i = 0; i < thirdInitInput; i++)
         {
            string[] inputs = ReadInput().Split(' ');
            int fifthInput = int.Parse(inputs[0]);
            int sixthInput = int.Parse(inputs[1]);
         }

         Console.WriteLine(commands[c]);
      }
   }

   private static string ReadInput()
   {
      var input = Console.ReadLine() ?? throw new InvalidOperationException("There is no input");
      Console.Error.WriteLine(input);
      return input;
   }
}