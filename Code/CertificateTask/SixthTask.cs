namespace CertificateTask;

internal class SixthTask
{
   #region Public Methods and Operators

   public void Execute()
   {
      string message = Console.ReadLine();

      var length = message.Length;

      var forSquare = (int)Math.Floor(Math.Sqrt(length));

      List<string> messageArray = new List<string>();

      for (int k = 0; k < length; k+=forSquare)
      {
         var subString = message.Substring(k, forSquare);
         messageArray.Add(subString);
      }

      for (int i = 0; i < messageArray.Count; i++)
      {
         for (int j = 0; j < messageArray[i].Length; j++)
         {
            Console.Write(messageArray[j][i]);
         }
      }

      Console.WriteLine();
   }

   #endregion
}