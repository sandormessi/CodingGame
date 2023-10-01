namespace Playground;

internal class Program
{
   #region Methods

   private static void Main(string[] args)
   {
      string simulatedInput = string.Empty;

      var simulatedInputTextReader = new StringReader(simulatedInput);
      Console.SetIn(simulatedInputTextReader);
   }

   private static string ReadFromFile(string filePath)
   {
      return File.ReadAllText(filePath);
   }

   #endregion
}