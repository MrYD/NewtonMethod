using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace SummerConsoleApplication
{
    class Program
    {
        private static void Main(string[] args)
        {
            var xpix = 1000;
            var ypix = 1000;

            var Buffer = new int[xpix][];
            for (var i = 0; i < Buffer.Length; i++)
            {
                Buffer[i] = new int[ypix];
            }
            var newton = new Newton(1e-12, 30,
                                    x => x * x * x - 1,
                                    x => 3.0 * x * x);
            Complex x1 = new Complex(1.0, 0.0);
            Complex x2 = new Complex(-0.5, Math.Sqrt(3.0) / 2.0);
            Complex x3 = new Complex(-0.5, -Math.Sqrt(3.0) / 2.0);
            var map = new Maping2D(2000, 2000, -2000, -2000, xpix - 1, ypix - 1, (x, y, i, j) =>
            {
                try
                {
                    var res = newton.StartRel(new Complex(x, y));
                    if ((res - x1).Magnitude < 1e-5)
                        Buffer[i][j] = 1;
                    else if ((res - x2).Magnitude < 1e-5)
                        Buffer[i][j] = 2;
                    else if ((res - x3).Magnitude < 1e-5)
                        Buffer[i][j] = 3;
                }
                catch (Exception)
                {
                    Buffer[i][j] = 0;
                }
            });
            map.Execute();

            Bitmap img = new Bitmap(xpix, ypix);


            for (int i = 0; i < Buffer.Length; i++)
            {
                var b = Buffer[i];
                for (int j = 0; j < b.Length; j++)
                {
                    var b1 = b[j];
                    switch (b1)
                    {
                        case 0:
                            img.SetPixel(xpix - i - 1, j, Color.Black);
                            break;
                        case 1:
                            img.SetPixel(xpix - i - 1, j, Color.Red);
                            break;
                        case 2:
                            img.SetPixel(xpix - i - 1, j, Color.Blue);
                            break;
                        case 3:
                            img.SetPixel(xpix - i - 1, j, Color.Green);
                            break;
                        default:
                            img.SetPixel(xpix - i - 1, j, Color.White);
                            break;
                    }
                }
            }
            img.Save(@"C:\Users\wang\Desktop\img8.bmp");
        }
    }
}

