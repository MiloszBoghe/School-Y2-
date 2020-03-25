using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare
{
    public interface ICalculator
    {
        double? ExecuteOperation(string operatorName, string[] arguments);
    }
}
