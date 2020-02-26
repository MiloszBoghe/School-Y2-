using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare.Controllers
{
    public class MathController
    {
        public double Add(double n, double n2)
        {
            return n + n2;
        }

        public double Subtract(double n, double n2)
        {
            return n - n2;
        }

        public double Divide(double n, double n2)
        {
            return n / n2;
        }

        public double Multiply(double n, double n2)
        {
            return n * n2;
        }

        public long Faculty(int n)
        {
            return n < 0 ? throw new Exception() : n == 0 ? 1 : n * Faculty(n - 1);
        }
    }
}
