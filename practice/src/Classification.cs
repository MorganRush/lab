using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.src
{
    public static class Classification
    {
        public static void DoFilterBulgeAndСoncavity(this TableFunction tableFunction)
        {
            int length = tableFunction.X.Length;
            double lastXStart = tableFunction.X[0];
            Func<double, double> func = x => (tableFunction.CoefficientD[0] * 6 * x + tableFunction.CoefficientC[0] * 2);
            BulgeAndСoncavity lastBulgeAndСoncavity;
      
            BulgeAndСoncavity bulgeAndСoncavity;

        }

        public static void DoFilterMonotony(this TableFunction tableFunction)
        {   
            int length = tableFunction.X.Length;
            double lastXStart = tableFunction.X[0];
            Monotony lastMonotony = CheckMonotony(tableFunction.Y[0], tableFunction.Y[1]);
            Monotony monotony = lastMonotony;

            for(int i = 2; i < length; i++)
            {
                monotony = CheckMonotony(tableFunction.Y[i-1], tableFunction.Y[i]);

                if (lastMonotony == monotony)
                    continue;
                else
                {
                    tableFunction.cutsMonotonies.Add(new CutMomotony(lastXStart, tableFunction.X[i-1], lastMonotony));
                    lastMonotony = monotony;
                    lastXStart = tableFunction.X[i];
                }
            }
            tableFunction.cutsMonotonies.Add(new CutMomotony(lastXStart, tableFunction.X[length - 1], lastMonotony));
        }
        
        private static Monotony CheckMonotony(double yStart, double yEnd)
        {
            if (yStart < yEnd)
                return Monotony.decreases;
            else if (yStart == yEnd)
                return Monotony.constant;
            else
                return Monotony.increases;
        }
    }
}
