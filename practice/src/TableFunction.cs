using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using practice.src.lab3;

namespace practice.src
{
    public enum Monotony { decreases, constant, increases };
    public enum BulgeAndСoncavity { bugle, concavity };

    public struct CutBugleAndConcavity
    {
        double xStart;
        double xEnd;
        BulgeAndСoncavity bulgeAndСoncavity;

        public CutBugleAndConcavity(double xStart, double xEnd, BulgeAndСoncavity bulgeAndСoncavity)
        {
            this.xStart = xStart;
            this.xEnd = xEnd;
            this.bulgeAndСoncavity = bulgeAndСoncavity;
        }
    }

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
    public class TableFunction : IFunctionContent
    {
        public double[] X { get; set; }
        public double[] Y { get; set; }
        public double[] CoefficientA { get; set; }
        public double[] CoefficientB { get; set; }
        public double[] CoefficientC { get; set; }
        public double[] CoefficientD { get; set; }

        public List<CutMomotony> cutsMonotonies;
        public BulgeAndСoncavity bulgeAndСoncavity;

        public TableFunction(double[] x, double[] y)
        {
            if (x.Length != y.Length)
                return;
            this.X = new double[x.Length];
            this.Y = new double[y.Length];
            for (int i = 0; i < x.Length; i++)
            {
                this.X[i] = x[i];
                this.Y[i] = y[i];
            }
        }
    }
}
