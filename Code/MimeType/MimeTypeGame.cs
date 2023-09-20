namespace MimeType;

public class MimeTypeGame
{
   #region Methods

   static void Main(string[] args)
   {
      int N = int.Parse(Console.ReadLine());
      int Q = int.Parse(Console.ReadLine());

      Dictionary<string, string> mimetypes = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
      for (int i = 0; i < N; i++)
      {
         string[] inputs = Console.ReadLine().Split(' ');
         string EXT = inputs[0];
         string MT = inputs[1];

         mimetypes.Add(EXT, MT);
      }

      for (int i = 0; i < Q; i++)
      {
         string FNAME = Console.ReadLine();
         var extension = Path.GetExtension(FNAME);
         if (string.IsNullOrWhiteSpace(extension))
         {
            Console.WriteLine("UNKNOWN");
            continue;
         }

         if (mimetypes.TryGetValue(extension.TrimStart('.'), out var mimeType))
         {
            Console.WriteLine(mimeType);
         }
         else
         {
            Console.WriteLine("UNKNOWN");
         }
      }
   }

   #endregion
}