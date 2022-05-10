using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Utils
{
    public static class WindowPos
    {
        public static List<Window> LX = new List<Window>();

        public static double WRight(Window w)
        {
            return w.Left + w.Width;
        }
        public static double WBottom(Window w)
        {
            return w.Top + w.Height;
        }

        public static void GetNearBy(Window n)
        {
            if (LX.Count <= 1)
            {
                return;
            }
            List<double> AT = new List<double>();
            List<double> AL = new List<double>();


            Dictionary<double,double> L = new Dictionary<double,double>();
            Dictionary<double,double> T = new Dictionary<double,double>();
            if (n == null)
            {
                return;
            }
            foreach (var w in LX)
            {
                if (w == n) { continue; }
                else
                {
                    var r = w.Left - n.Left;

                    L[Math.Abs(r)] = r;

                    AL.Add(Math.Abs(r));
                    //L.Add(r);
                    Console.WriteLine($"Left:{r}");
                }
            }
            foreach (var w in LX)
            {
                if (w == n) { continue; }
                else
                {
                    var r= WBottom(w) - n.Top;
                    T[Math.Abs(r)] = r;

                    AT.Add(Math.Abs(r));
                    //T.Add(r);
                    Console.WriteLine($"Top:{r}");

                }

            }
            if (AT.Min() <= 20)
            {
                n.Top = T[AT.Min()] + n.Top + 8;
            }
            if (AL.Min() <= 8)
            {
                n.Left = L[AL.Min()] + n.Left;
            }
        }
    }
}
