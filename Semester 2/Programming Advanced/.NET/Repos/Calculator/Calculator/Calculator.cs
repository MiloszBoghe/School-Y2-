using System;

namespace Calculator
{
    public static class Calculator
    {
        public static int Addition(int a, int b)
        {
            return a + b;
        }

        public static long Faculty(long f)
        {
            return f < 1 ? throw new Exception("too smaaaall!") : f == 0 ? 1 : f * Faculty(f - 1);
        }

    }

}