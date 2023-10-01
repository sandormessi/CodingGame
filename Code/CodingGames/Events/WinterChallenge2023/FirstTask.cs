namespace CodingGames.Events.WinterChallenge2023;

using System.Text;

internal class FirstTask
{
   #region Public Methods and Operators

   public static string Decrypt(string s1, string s2)
   {
      var lines = new[] { s1, s2 };

      StringBuilder decryptStringBuilder = new();

      for (var j = 0; j < lines[0].Length; j++)
      {
         foreach (var line in lines)
         {
            decryptStringBuilder.Append(line[j]);
         }
      }

      return decryptStringBuilder.ToString();
   }

   #endregion
}