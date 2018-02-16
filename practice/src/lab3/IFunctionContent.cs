using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.src.lab3
{
    public interface IFunctionContent
    {
        double[] X { get; set; }                                   // табличные значения
        double[] Y { get; set; }                                   // табличные значения
        double[] CoefficientA { get; set; }                        // нулевой коэффициенты многочлена 3-й степени
        double[] CoefficientB { get; set; }                        // единичный коэффициенты многочлена 3-й степени
        double[] CoefficientC { get; set; }                        // квадратичный коэффициенты многочлена 3-й степени
        double[] CoefficientD { get; set; }                        // кубический коэффициенты многочлена 3-й степени
    }
}
