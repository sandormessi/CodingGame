namespace CodingGames.Puzzles.Medium;

using System.Text;

public class MayanCalculation
{
   #region Constants and Fields

   private static readonly List<MayanCharacter> characters = new(20);

   #endregion

   #region Public Methods and Operators

   public static void Main()
   {
      string[] inputs = ReadInput().Split(' ');

      int characterWidth = int.Parse(inputs[0]);
      int characterHeight = int.Parse(inputs[1]);

      List<string> mayanAbc = new(characterHeight);
      for (int i = 0; i < characterHeight; i++)
      {
         string numeral = ReadInput();
         mayanAbc.Add(numeral);
      }

      for (int i = 0; i < 20; i++)
      {
         var start = i * characterWidth;
         var end = start + characterWidth;

         List<string> lines = new(mayanAbc.Count);
         foreach (var mayanAbcLine in mayanAbc)
         {
            var line = mayanAbcLine[start..end];
            lines.Add(line);
         }

         characters.Add(new MayanCharacter(lines.Select(x => x.ToArray()).ToArray()));
      }

      int S1 = int.Parse(ReadInput());
      List<MayanCharacter> number1Characters = new();
      for (int k = 0; k < S1 / characterHeight; k++)
      {
         List<string> s1Lines = new(S1);
         for (int i = 0; i < characterHeight; i++)
         {
            string num1Line = ReadInput();
            s1Lines.Add(num1Line);
         }

         number1Characters.Add(new MayanCharacter(s1Lines.Select(x => x.ToArray()).ToArray()));
      }

      var number1Value = ConvertToDecimal(number1Characters);

      int S2 = int.Parse(ReadInput());
      List<MayanCharacter> number2Characters = new();
      for (int k = 0; k < S2 / characterHeight; k++)
      {
         List<string> s2Lines = new(S1);
         for (int i = 0; i < characterHeight; i++)
         {
            string num2Line = ReadInput();
            s2Lines.Add(num2Line);
         }

         number2Characters.Add(new MayanCharacter(s2Lines.Select(x => x.ToArray()).ToArray()));
      }

      var number2Value = ConvertToDecimal(number2Characters);

      string operation = ReadInput();
      long result = long.MinValue;
      switch (operation)
      {
         case "+":
            result = number1Value + number2Value;
            break;

         case "-":
            result = number1Value - number2Value;
            break;

         case "*":
            result = number1Value * number2Value;
            break;

         case "/":
            result = number1Value / number2Value;
            break;
      }

      var convertedNumber = ConvertTo20NumericSystem(result);
      Console.Error.WriteLine("Mayan characters:");

      foreach (var i in convertedNumber)
      {
         Console.Write(characters[Math.Abs(i)]);
      }

      Console.Error.WriteLine("Result of multiplication in decimal:");
      Console.Error.WriteLine(result);
   }

   #endregion

   #region Methods

   private static IReadOnlyList<int> ConvertTo20NumericSystem(long number)
   {
      List<int> digits = new(16);
      if (number == 0)
      {
         digits.Add(0);
         return digits;
      }

      while (number > 0)
      {
         var quotient = number / 20;
         var reminder = number % 20;
         digits.Add((int)reminder);
         number = quotient;
      }

      digits.Reverse();

      Console.Error.WriteLine("Mayan digits:");
      foreach (var digit in digits)
      {
         Console.Error.Write($"{digit} ");
      }

      Console.Error.WriteLine();
      return digits;
   }

   private static long ConvertToDecimal(IReadOnlyList<MayanCharacter> charactersToConvert)
   {
      Console.Error.WriteLine("Converting to decimal:");

      foreach (var mayanCharacter in charactersToConvert)
      {
         Console.Error.Write(mayanCharacter.ToString());
      }

      long power = charactersToConvert.Count - 1;
      long decimalNumber = 0;
      foreach (var mayanCharacter in charactersToConvert)
      {
         var characterValue = characters.FindIndex(x => x.Equals(mayanCharacter));
         decimalNumber += characterValue * (long)Math.Pow(20, power);
         power--;
      }

      Console.Error.WriteLine($"In Decimal: '{decimalNumber}'.");
      Console.Error.WriteLine("---------------------");
      return decimalNumber;
   }

   private static string ReadInput()
   {
      var readInput = Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
      return readInput;
   }

   #endregion

   private sealed class MayanCharacter : IEquatable<MayanCharacter>
   {
      #region Constructors and Destructors

      public MayanCharacter(char[][] characters)
      {
         Characters = characters;
      }

      #endregion

      #region IEquatable<MayanCharacter> Members

      public bool Equals(MayanCharacter? other)
      {
         if (other is null)
         {
            return false;
         }

         for (int i = 0; i < Characters.Length; i++)
         {
            for (int j = 0; j < Characters[i].Length; j++)
            {
               if (Characters[i][j] != other.Characters[i][j])
               {
                  return false;
               }
            }
         }

         return true;
      }

      #endregion

      #region Public Properties

      public char[][] Characters { get; }

      #endregion

      #region Public Methods and Operators

      public override string ToString()
      {
         StringBuilder st = new(256);
         foreach (var character in Characters)
         {
            st.AppendLine(string.Join(string.Empty, character));
         }

         return st.ToString();
      }

      #endregion
   }
}