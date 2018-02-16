using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.src
{
    public enum Monotony { decreases, constant, increases };
    public enum BulgeAndСoncavity { bugle, concavity, nothing };

    public struct CutMomotony
    {
        double xStart;
        double xEnd;
        Monotony monotony;

        public CutMomotony(double xStart, double xEnd, Monotony monotony)
        {
            this.xStart = xStart;
            this.xEnd = xEnd;
            this.monotony = monotony;
        }
    }
    public class TableFunction
    {
        public double[] x;
        public double[] y;
        public List<CutMomotony> cutsMonotonies;
        public BulgeAndСoncavity bulgeAndСoncavity;

        public TableFunction(double[] x, double[] y)
        {
            if (x.Length != y.Length)
                return;
            this.x = new double[x.Length];
            this.y = new double[y.Length];
            for (int i = 0; i < x.Length; i++)
            {
                this.x[i] = x[i];
                this.y[i] = y[i];
            }
        }
    }
}
