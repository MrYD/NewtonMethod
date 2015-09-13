using System;

namespace SummerConsoleApplication
{
    public class Maping2D
    {
        private int _x;
        private int _y;
        private double _xsize;
        private double _ysize;
        private double _x0;
        private double _y0;
        private Action<double, double, int, int> _action;

        public Maping2D(double topx, double topy, double botx, double boty, int divx, int divy, Action<double, double,int,int> action)
        {
            _x = divx;
            _y = divy;
            _x0 = topx;
            _y0 = topy;
            _xsize = (botx - topx) / divx;
            _ysize = (boty - topy) / divy;
            _action = action;
        }

        public void Execute()
        {
            for (int i = 0; i <= _x; i++)
            {
                for (int j = 0; j <= _y; j++)
                {
                    _action(_x0 + i * _xsize, _y0 + j * _ysize, i, j);
                }
            }
        }

    }


}
