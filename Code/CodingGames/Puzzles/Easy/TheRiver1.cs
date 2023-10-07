namespace CodingGames.Puzzles.Easy;

public class TheRiver1
{
   #region Methods

   private static long FindMeetingPoint(IEnumerable<long> river1, List<long> river2)
   {
      foreach (long possibleMeetingPoint in river1)
      {
         int result = river2.BinarySearch(possibleMeetingPoint);
         if (result >= 0)
         {
            return possibleMeetingPoint;
         }
      }

      return long.MinValue;
   }

   private static void GetDigits(ICollection<long> digits, long number)
   {
      while (true)
      {
         if (number < 10)
         {
            digits.Add(number);
            return;
         }

         long number2 = number / 10;
         long remaining = number - number2 * 10;
         digits.Add(remaining);

         number = number2;
      }
   }

   private static void Main()
   {
      long r1 = long.Parse(Console.ReadLine());
      long r2 = long.Parse(Console.ReadLine());

      long previous1 = r1;
      long previous2 = r2;

      List<long> river1 = new() { previous1 };
      List<long> river2 = new() { previous2 };

      var digits1 = new List<long>(10);
      var digits2 = new List<long>(10);

      long meetingPoint;
      while (true)
      {
         digits1.Clear();
         digits2.Clear();

         GetDigits(digits1, previous1);
         GetDigits(digits2, previous2);

         long nex1 = previous1 + digits1.Sum();
         long nex2 = previous2 + digits2.Sum();

         previous1 = nex1;
         previous2 = nex2;

         river1.Add(previous1);
         river2.Add(previous2);

         meetingPoint = FindMeetingPoint(river1, river2);

         if (meetingPoint != long.MinValue)
         {
            break;
         }
      }

      Console.WriteLine(meetingPoint);
   }

   #endregion
}