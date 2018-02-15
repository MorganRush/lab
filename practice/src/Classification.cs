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
            int length = tableFunction.x.Length;
            double lastXStart = tableFunction.x[0];
            Monotony lastMonotony;
            Monotony monotony;
        
            if (tableFunction.y[0] < tableFunction.y[1])
                lastMonotony = Monotony.decreases;
            else if (tableFunction.y[0] == tableFunction.y[1])
                lastMonotony = Monotony.constant;
            else
                lastMonotony = Monotony.increases;

            //tableFunction.cutsMonotonies.Add(new CutMomotony(tableFunction.x[0], tableFunction.x[1], lastMonotony));

            for(int i = 2; i < length; i++)
            {
                if (tableFunction.x[i - 1] < tableFunction.x[i])
                    monotony = Monotony.decreases;
                else if (tableFunction.x[i - 1] == tableFunction.x[i])
                    monotony = Monotony.constant;
                else
                    monotony = Monotony.increases;

                if (lastMonotony == monotony)
                    continue;
                else
                {
                    tableFunction.cutsMonotonies.Add(new CutMomotony(lastXStart, tableFunction.x[i-1], lastMonotony));
                    lastMonotony = monotony;
                    lastXStart = tableFunction.x[i];
                }
            }
            tableFunction.cutsMonotonies.Add(new CutMomotony(lastXStart, tableFunction.x[length - 1], lastMonotony));
        }
    }
}
