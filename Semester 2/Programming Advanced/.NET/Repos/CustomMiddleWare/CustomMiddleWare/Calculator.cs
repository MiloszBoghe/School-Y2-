using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare
{
    public class Calculator : ICalculator
    {
        public double? ExecuteOperation(string operatorName, string[] arguments)
        {
            if (arguments.Length == 0)
            {
                return null;
            }

            var digits = arguments.ToList().Select(int.Parse).ToList();

            switch (operatorName)
            {
                case "add":
                    return digits.Sum();
                case "subtract":
                    return -digits.Sum();
                case "factorial":
                    if (digits.Count == 1)
                    {
                        int result = digits[0];
                        for (int i = 1; i < digits[0]; i++)
                        {
                            result *= i;
                        }
                        return result;
                    }
                    break;
            }

            return null;
        }
    }
}
