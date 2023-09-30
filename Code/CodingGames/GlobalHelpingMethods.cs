namespace CodingGames;

internal class GlobalHelpingMethods
{
   #region Methods

   private static void DebugMessage<T>(T message)
   {
      Console.Error.Write(message);
   }

   private static void DebugMessageLine<T>(T? message)
   {
      Console.Error.WriteLine(message);
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is not input.");
   }

   private static void WriteOutput<T>(T answer)
   {
      Console.Write(answer);
   }

   private static void WriteOutputLine<T>(T? answer)
   {
      Console.WriteLine(answer);
   }

   #endregion
}