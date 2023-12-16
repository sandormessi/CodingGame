namespace CodingGames.Certificates.SecondTry;

using System;
using System.Collections.Generic;
using System.Linq;

internal class FirstTask
{
   #region Public Methods and Operators

   public void Execute()
   {
      string readInput = ReadInput();
      int numberOfCars = int.Parse(readInput);

      List<CarParkTicket> carParkTickets = new();

      for (int i = 0; i < numberOfCars; i++)
      {
         var carTicketInput = ReadInput();
         string[] carTicketStringInputs = carTicketInput.Split(' ');

         int enterPark = int.Parse(carTicketStringInputs[0]);
         int exitPark = int.Parse(carTicketStringInputs[1]);

         carParkTickets.Add(new CarParkTicket(enterPark, exitPark));
      }

      List<int> hours = new();

      foreach (var carParkTicket in carParkTickets)
      {
         for (int i = carParkTicket.Enter; i < carParkTicket.Exit; i++)
         {
            hours.Add(i);
         }
      }

      var distinchours = hours.Distinct();

      Console.WriteLine(distinchours.Count());
   }

   #endregion

   #region Methods

   private static string ReadInput()
   {
      var readValue = Console.ReadLine();
      if (readValue is null)
      {
         throw new InvalidOperationException("There is no input.");
      }

      return readValue;
   }

   #endregion

   private class CarParkTicket
   {
      #region Constructors and Destructors

      public CarParkTicket(int enter, int exit)
      {
         Enter = enter;
         Exit = exit;
      }

      #endregion

      #region Public Properties

      public int Enter { get; }

      public int Exit { get; }

      #endregion
   }
}