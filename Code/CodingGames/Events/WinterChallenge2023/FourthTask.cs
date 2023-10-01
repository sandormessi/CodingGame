namespace CodingGames.Events.WinterChallenge2023;

using System.Text;

internal class FourthTask
{
   #region Public Methods and Operators

   public static string Decrypt(string firstA, string firstB, string secondB, string secondC, string secretC)
   {
      Dictionary<char, char> AtoB = new();

      for (int i = 0; i < firstA.Length; i++)
      {
         if (AtoB.ContainsKey(firstA[i]))
         {
            continue;
         }

         AtoB.Add(firstA[i], firstB[i]);
      }

      Dictionary<char, char> BtoC = new();

      for (int i = 0; i < secondB.Length; i++)
      {
         if (BtoC.ContainsKey(secondB[i]))
         {
            continue;
         }

         BtoC.Add(secondB[i], secondC[i]);
      }

      StringBuilder messageStringBuilder = new();

      for (int i = 0; i < secretC.Length; i++)
      {
         var actualSecretCharacter = secretC[i];

         var translateToB = BtoC.First(x => x.Value == actualSecretCharacter).Key;
         var translateToA = AtoB.First(x => x.Value == translateToB).Key;

         messageStringBuilder.Append(translateToA);
      }

      return messageStringBuilder.ToString();
   }

   #endregion
}