namespace CodingGames.Puzzles.Medium;

using System;
using System.Collections.Generic;

public class ConwaySequence
{
   #region Public Methods and Operators

   public static void Main()
   {
      int R = int.Parse(ReadInput());
      int L = int.Parse(ReadInput());

      int initialNumber = R;

      List<int> actualLine = new(128) { initialNumber };

      for (int i = 1; i < L; i++)
      {
         var groups = GetNumberGroupsOfLine(actualLine);
         actualLine.Clear();
         foreach (var group in groups)
         {
            actualLine.Add(group.Item2);
            actualLine.Add(group.Item1);
         }
      }

      Console.WriteLine(string.Join(' ', actualLine));
   }

   #endregion

   #region Methods

   private static IReadOnlyList<Tuple<int, int>> GetNumberGroupsOfLine(IReadOnlyList<int> line)
   {
      List<Tuple<int, int>> numberGroups = new();

      for (int i = 0; i < line.Count;)
      {
         var actualCharacter = line[i];
         var actualIndex = i;
         while (i < line.Count && line[i] == actualCharacter)
         {
            i++;
         }

         numberGroups.Add(new Tuple<int, int>(actualCharacter, i - actualIndex));
      }

      return numberGroups;
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   #endregion
}