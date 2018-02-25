using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.src
{
    public static class Classification
    {
        const int cut = 10;



        public static void DoFilterBulgeAndСoncavity(this TableFunction tableFunction)
        {
            int length = tableFunction.CoefficientA.Length;
            double lastXStart = tableFunction.X[0];
            Func<double, double> func = x => 
            (tableFunction.CoefficientD[0] * 6 * x + tableFunction.CoefficientC[0] * 2);
            BulgeAndСoncavity lastBulgeAndСoncavity = CheckBulgeAndСoncavity(tableFunction.Y[0]);

            if (lastBulgeAndСoncavity == BulgeAndСoncavity.inflection)
                lastBulgeAndСoncavity = CheckBulgeAndСoncavity(tableFunction.X[0] +
                    (tableFunction.X[1] - tableFunction.X[0]) / cut);
            BulgeAndСoncavity bulgeAndСoncavity = lastBulgeAndСoncavity;

            for (int i = 1; i < length; i++)
            {
                double delta = (tableFunction.X[i] - tableFunction.X[i-1]) / cut;
                for (int j = i; j < 10; j++)
                {
                    bulgeAndСoncavity = CheckBulgeAndСoncavity(func(tableFunction.X[j - 1] + j * delta));
                    if (lastBulgeAndСoncavity == bulgeAndСoncavity)
                        continue;
                    else
                    {
                        tableFunction.cutsBulgeAndСoncavity.Add(new CutBugleAndConcavity(lastXStart,
                            tableFunction.X[j - 1] + j * delta / 2, lastBulgeAndСoncavity));
                        lastBulgeAndСoncavity = bulgeAndСoncavity;
                        lastXStart = tableFunction.X[j - 1] + j * delta / 2;
                    }
                }
                func = x => (tableFunction.CoefficientD[i] * 6 * x + tableFunction.CoefficientC[i] * 2);
            }
            tableFunction.cutsBulgeAndСoncavity.Add(new CutBugleAndConcavity(lastXStart,
                tableFunction.X[length], lastBulgeAndСoncavity));
        }

        private static BulgeAndСoncavity CheckBulgeAndСoncavity(double y)
        {
            if (y < 0)
                return BulgeAndСoncavity.bugle;
            else if (y == 0)
                return BulgeAndСoncavity.inflection;
            else
                return BulgeAndСoncavity.concavity;
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
                    tableFunction.cutsMonotonies.Add(new CutMomotony(lastXStart, tableFunction.X[i-1], 
                        lastMonotony));
                    lastMonotony = monotony;
                    lastXStart = tableFunction.X[i];
                }
            }
            tableFunction.cutsMonotonies.Add(new CutMomotony(lastXStart, tableFunction.X[length - 1], 
                lastMonotony));
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
