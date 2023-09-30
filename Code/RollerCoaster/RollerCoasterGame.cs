namespace RollerCoaster;

public class RollerCoasterGame
{
   private sealed class Group
   {
      public readonly int Id;

      public readonly int Size;

      public Group(int id, int size)
      {
         Id = id;
         Size = size;
      }
   }

   public static void Main()
   {
      var inputs = ReadInput().Split(' ');

      var seatCount = int.Parse(inputs[0]);
      var roundCount = int.Parse(inputs[1]);
      var groupCount = int.Parse(inputs[2]);

      List<Group> queue = new(groupCount);
      for (var i = 0; i < groupCount; i++)
      {
         var groupSize = int.Parse(ReadInput());
         queue.Add(new Group(i, groupSize));
      }

      long earning = 0;

      List<Group> selectedGroupsForRide = new(groupCount);
     
      earning = Ride(selectedGroupsForRide, seatCount, queue, earning);

      for (var i = 1; i < roundCount; i++)
      {
         earning = Ride(selectedGroupsForRide, seatCount, queue, earning);

         if (queue[0].Id == 0)
         {
            // We came around
           
            var roundSoFar = i + 1;

            var groupRound = roundCount / roundSoFar;
            earning *= groupRound;
            var remaining = roundCount % roundSoFar;

            Console.Error.WriteLine($"We came around. {groupRound}, {remaining}");

            for (var j = 0; j < remaining; j++)
            {
               earning = Ride(selectedGroupsForRide, seatCount, queue, earning);
            }

            break;
         }
      }

      Console.WriteLine(earning);
   }

   private static long Ride(ICollection<Group> selectedGroupsForRide, int seatCount, List<Group> queue, long earning)
   {
      selectedGroupsForRide.Clear();

      var remainingSeats = seatCount;
      foreach (var actualGroup in queue)
      {
         remainingSeats -= actualGroup.Size;

         if (remainingSeats >= 0)
         {
            earning += actualGroup.Size;
            selectedGroupsForRide.Add(actualGroup);
         }
         else
         {
            break;
         }
      }

      queue.RemoveRange(0, selectedGroupsForRide.Count);
      queue.AddRange(selectedGroupsForRide);

      return earning;
   }

   private static string ReadInput()
   {
      return ReadInputFromFile();
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   private static StreamReader? streamReader;
   private static string ReadInputFromFile()
   {
      if (streamReader == null)
      {
         var filePath = @"C:\Users\Sándor\Desktop\input.txt";
         streamReader = new StreamReader(filePath);
      }

      return streamReader.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }
}