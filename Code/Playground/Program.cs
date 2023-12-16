
using CodingGames.Puzzles.Medium;

namespace Playground;

internal class Program
{
   #region Methods

   private static void Main(string[] args)
   {
      string simulatedInput =
         "10 10\r\n##########\r\n#        #\r\n#  @     #\r\n#  B     #\r\n#  S   W #\r\n# XXX    #\r\n#  B   N #\r\n# XXXXXXX#\r\n#       $#\r\n##########";

      var simulatedInputTextReader = new StringReader(simulatedInput);
      Console.SetIn(simulatedInputTextReader);

      BlunderEpisode1.Main();
   }

   private static string ReadFromFile(string filePath)
   {
      return File.ReadAllText(filePath);
   }

   #endregion
}