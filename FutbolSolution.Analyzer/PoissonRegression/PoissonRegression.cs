using System;
namespace FutbolSolution.Analyzer.PoissonRegression
{
    public class PoissonRegression
    {
        private double[] coefficients;
        private double intercept;

        // Constructor to initialize coefficients and intercept
        public PoissonRegression(double[] coefficients, double intercept)
        {
            this.coefficients = coefficients;
            this.intercept = intercept;
        }

        // Poisson link function (log)
        private double LinkFunction(double x)
        {
            return Math.Exp(x);
        }

        // Predict expected count
        public double PredictCount(double[] features)
        {
            if (coefficients.Length != features.Length)
            {
                throw new ArgumentException("Coefficients and features must be of the same length.");
            }

            double linearCombination = intercept;
            for (int i = 0; i < coefficients.Length; i++)
            {
                linearCombination += coefficients[i] * features[i];
            }

            return LinkFunction(linearCombination);
        }
    }

}
