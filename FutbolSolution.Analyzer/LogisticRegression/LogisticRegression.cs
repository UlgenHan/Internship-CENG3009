using System;
namespace FutbolSolution.Analyzer.LogisticRegression
{
    public class LogisticRegression
    {
        private double[] coefficients;
        private double intercept;

        // Constructor to initialize coefficients and intercept
        public LogisticRegression(double[] coefficients, double intercept)
        {
            this.coefficients = coefficients;
            this.intercept = intercept;
        }

        // Logistic function
        private double LogisticFunction(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        // Predict probability
        public double PredictProbability(double[] features)
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

            return LogisticFunction(linearCombination);
        }

        // Predict outcome based on threshold
        public int PredictOutcome(double probability, double threshold = 0.5)
        {
            return probability >= threshold ? 1 : 0;
        }
    }
}
