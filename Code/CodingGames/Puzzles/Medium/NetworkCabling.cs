namespace CodingGames.Puzzles.Medium;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class NetworkCabling
{
   #region Methods

   static void Main()
   {
      int n = int.Parse(Console.ReadLine());
      List<Point> p = new(n);
      for (var i = 0; i < n; i++)
      {
         var b = Console.ReadLine().Split(' ');
         p.Add(new(int.Parse(b[0]), int.Parse(b[1])));
      }

      var d = p.OrderBy(x => x.Y).ToArray();
      int k = d.Length;
      int h = k / 2;
      var j = k % 2 == 0 ? (d[h].Y + d[h - 1].Y) / 2d : d[h].Y;
      var m = (int)Math.Round(j);
      m = d.MinBy(x => Math.Abs(x.Y - m)).Y;
      long l = d.Aggregate<Point, long>(0, (e, f) => e + Math.Abs(f.Y - m));
      l += p.Max(p => p.X) - p.Min(p => p.X);
      Console.WriteLine(l);
   }

   #endregion
}