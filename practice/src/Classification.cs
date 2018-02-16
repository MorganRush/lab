using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.src
{
    public static class Classification
    {
        public static void DoFilterMonotony(this TableFunction tableFunction)
        {   
            int length = tableFunction.y.Length;
            double lastYStart = tableFunction.y[0];
            Monotony lastMonotony;
            Monotony monotony;

            lastMonotony = CheckMonotony(tableFunction.y[0], tableFunction.y[1]);

            for(int i = 2; i < length; i++)
            {
                monotony = CheckMonotony(tableFunction.y[i-1], tableFunction.y[i]);

                if (lastMonotony == monotony)
                    continue;
                else
                {
                    tableFunction.cutsMonotonies.Add(new CutMomotony(lastYStart, tableFunction.y[i-1], lastMonotony));
                    lastMonotony = monotony;
                    lastYStart = tableFunction.y[i];
                }
            }
            tableFunction.cutsMonotonies.Add(new CutMomotony(lastYStart, tableFunction.y[length - 1], lastMonotony));
        }
        
        private static Monotony CheckMonotony(double start, double end)
        {
            if (start < end)
                return Monotony.decreases;
            else if (start == end)
                return Monotony.constant;
            else
                return Monotony.increases;
        }
    }
}
