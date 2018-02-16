using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.src.lab3
{
    public static class Interpolation
    {
        static public IFunctionContent FindCoefficients(this IFunctionContent content)
        {
            int countPoint = content.X.Length;
            double[] stepX;
            double[] stepYdivStepX;
            double[] delta;
            double[] lambda;
            stepX = new double[countPoint - 1];
            stepYdivStepX = new double[countPoint - 1];
            delta = new double[countPoint - 1];
            lambda = new double[countPoint - 1];
            for (int i = 0; i < countPoint - 1; i++)
            {
                if (content.X[i] > content.X[i + 1])
                    throw new Exception("Значения x д.б. в порядке возрастания");
            }
            for (int i = 1; i < countPoint; i++)
            {
                stepX[i - 1] = content.X[i] - content.X[i - 1];
                if (stepX[i - 1] == 0)
                    throw new Exception("Значения x не должны совпадать");
                stepYdivStepX[i - 1] = (content.Y[i] - content.Y[i - 1]) / stepX[i - 1];
            }
            delta[0] = -stepX[1] / (2 * (stepX[0] + stepX[1]));
            lambda[0] = 1.5 * (stepYdivStepX[1] - stepYdivStepX[0]) / (stepX[0] + stepX[1]);
            for (int i = 2; i < countPoint - 1; i++)
            {
                delta[i - 1] = -stepX[i] /
                    (2 * stepX[i - 1] + 2 * stepX[i] + stepX[i - 1] * delta[i - 2]);
                lambda[i - 1] = (3 * stepYdivStepX[i] - 3 * stepYdivStepX[i - 1] - stepX[i - 1] * lambda[i - 2]) /
                    (2 * stepX[i - 1] + 2 * stepX[i] + stepX[i - 1] * delta[i - 2]);
            }
            content.CoefficientC[countPoint - 2] = 0;
            for (int i = countPoint - 2; i > 0; i--)
            {
                content.CoefficientC[i - 1] = delta[i - 1] * content.CoefficientC[i] + lambda[i - 1];
            }
            content.CoefficientD[0] = (content.CoefficientC[0]) / (3 * stepX[0]);
            content.CoefficientB[0] = stepYdivStepX[0] + (2 * content.CoefficientC[0] * stepX[0]) / 3;
            for (int i = 1; i < countPoint - 1; i++)
            {
                content.CoefficientD[i] = (content.CoefficientC[i] - content.CoefficientC[i - 1]) / (3 * stepX[i]);
                content.CoefficientB[i] = stepYdivStepX[i] +
                    (2 * content.CoefficientC[i] * stepX[i] + stepX[i] * content.CoefficientC[i - 1]) / 3;
            }
            return content;
        }
    }
}
