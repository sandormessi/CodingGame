namespace Defibrillators;

public class DefibrillatorsGame
{
   private static double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2)
   {
      longitude1 = ConvertDegreeToRadian(longitude1);
      longitude2 = ConvertDegreeToRadian(longitude2);

      latitude1 = ConvertDegreeToRadian(latitude1);
      latitude2 = ConvertDegreeToRadian(latitude2);

      var x = (longitude2 - longitude1) * Math.Cos((latitude1 + latitude2) / 2);
      var y = latitude2 - latitude1;

      return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) * 6371;
   }

   private static double ConvertDegreeToRadian(double degree)
   {
      return degree * Math.PI / 180;
   }

   private static double ConvertStringToFloat(string floatNumber)
   {
      return double.Parse(floatNumber.Replace(',', '.'));
   }

   static void Main()
   {
      double longitude = ConvertStringToFloat(Console.ReadLine());
      double latitude = ConvertStringToFloat(Console.ReadLine());

      int n = int.Parse(Console.ReadLine());

      List<Defibrillator> defibrillators = new(n);
      for (int i = 0; i < n; i++)
      {
         string defibrillatorData = Console.ReadLine();
         var data = defibrillatorData.Split(';');

         Defibrillator defibrillator = new(int.Parse(data[0]), data[1], data[2], data[3], ConvertStringToFloat(data[4]),
            ConvertStringToFloat(data[5]));

         defibrillators.Add(defibrillator);
      }

      var ordered = defibrillators.OrderBy(x => CalculateDistance(x.Longitude, x.Latitude, longitude, latitude));

      Console.WriteLine(ordered.First().Name);
   }

   private sealed class Defibrillator
   {
      public Defibrillator(int id, string name, string address, string contactNumber, double longitude, double latitude)
      {
         Name = name;
         Address = address;
         ContactNumber = contactNumber;
         Longitude = longitude;
         Latitude = latitude;
         Id = id;
      }

      public string Address { get; }

      public string ContactNumber { get; }

      public int Id { get; }

      public double Latitude { get; }

      public double Longitude { get; }

      public string Name { get; }
   }
}