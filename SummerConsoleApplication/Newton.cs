using System;
using System.Collections.Generic;
using System.Numerics;

namespace SummerConsoleApplication
{
    public class Newton
    {
        private Func<Complex, Complex> df; //
        private double eps;
        private Func<Complex, Complex> f;
        private int max;
        private Exception _exeption;

        public Newton(double eps, int max, Func<Complex, Complex> f, Func<Complex, Complex> df)
        {
            this.eps = eps;
            this.max = max;
            this.f = f;
            this.df = df;
            this._exeption = new Exception("Not Converge");
        }

        public bool CheckConvergeAbs(Complex x)
        {
            int count = 0;
            while (f(x).Magnitude > eps)
            {
                count++;
                if (count >= max)
                {
                    return false;
                }
                x -= f(x) / df(x);
            }
            return true;
        }

        public bool CheckConvergeRel(Complex x)
        {

            int count = 0;
            while (f(x).Magnitude > eps)
            {
                count++;
                if (count >= max)
                {
                    return false;
                }
                x -= f(x) / df(x);
            }
            return true;
        }

        public Complex StartAbs(Complex x)
        {
            int count = 0;
            while (f(x).Magnitude > eps)
            {
                count++;
                if (count >= max)
                {
                    throw _exeption;
                }
                x -= f(x) / df(x);
            }
            return x;
        }

        public Complex StartRel(Complex x)
        {
            int count = 1;
            Complex x1 = x;
            Complex x2 = x1 - f(x1) / df(x1);
            while (((x2 - x1).Magnitude / x2.Magnitude) > eps)
            {
                count++;
                if (count >= max)
                {
                    throw _exeption;
                }
                x1 = x2;
                x2 = x1 - f(x1) / df(x1);
            }
            return x2;
        }

        public ResultObject StartAbsWithLog(Complex x)
        {
            ResultObject res = new ResultObject();

            int count = 0;
            res.LoopLog.Add(x);

            while (f(x).Magnitude > eps)
            {
                count++;
                if (count >= max)
                {
                    return ResultObject.NotConverge;
                }
                x -= f(x) / df(x);
                res.LoopLog.Add(x);
            }
            res.ConvergenceValue = x;
            res.LoopCount = count;
            return res;
        }

        public ResultObject StartRelWithLog(Complex x)
        {
            ResultObject res = new ResultObject();

            int count = 0;
            res.LoopLog.Add(x);

            count++;
            Complex x1 = x;
            Complex x2 = x1 - f(x1) / df(x1);
            res.LoopLog.Add(x2);

            while (((x2 - x1).Magnitude / x2.Magnitude) > eps)
            {
                count++;
                if (count >= max)
                {
                    return ResultObject.NotConverge;
                }
                x1 = x2;
                x2 = x1 - f(x1) / df(x1);
                res.LoopLog.Add(x2);
            }
            res.ConvergenceValue = x2;
            res.LoopCount = count;
            return res;
        }
    }

    public class ResultObject
    {
        private ResultState _resultState;

        public ResultObject()
        {
            LoopLog = new List<Complex>();
        }
        public static ResultObject NotConverge
        {
            get
            {
                return new ResultObject { ResultState = ResultState.NotConverge };
            }
        }
        public List<Complex> LoopLog { get; set; }
        public Complex ConvergenceValue { get; set; }
        public int LoopCount { get; set; }

        public ResultState ResultState
        {
            get { return _resultState; }
            set
            {
                _resultState = value;
                ResultState = ResultState.Commpleted;
            }
        }

        public override string ToString()
        {
            if (ResultState == ResultState.Commpleted)
            {
                return ConvergenceValue.ToString();
            }
            else
            {
                return ResultState.ToString();
            }
        }
    }

    public enum ResultState
    {
        Commpleted,
        NotConverge
    }
}
