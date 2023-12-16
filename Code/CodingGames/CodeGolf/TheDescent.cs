namespace CodingGames.CodeGolf;

using System;
using System.Linq;

class TheDescent
{
   #region Methods

   static void Main()
   {
      while (true)
      {
         int[] h = new int[8];
         for (int i = 0; i < 8; i++)
         {
            h[i] = int.Parse(Console.ReadLine());
         }

         Console.WriteLine(Array.IndexOf(h, h.Max()));
      }
   }

   #endregion
}