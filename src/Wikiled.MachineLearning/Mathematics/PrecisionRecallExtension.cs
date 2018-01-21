namespace Wikiled.MachineLearning.Mathematics
{
    public static class PrecisionRecallExtension
    {
        public static string GetTotalAccuracy(this PrecisionRecallCalculator<bool> calculator)
        {
            return $"Total:<{calculator.Total}> Positive:<{calculator.GetSingleAccuracy(true) * 100:F3}%> Negative:<{calculator.GetSingleAccuracy(false) * 100:F3}%> F1:<{calculator.F1(true):F3}>";
        }

        /// <summary>
        ///     Typical table is    || TP || FN ||
        ///     || FP || TN ||
        /// </summary>
        /// <param name="truePositives"></param>
        /// <param name="falseNegatives"></param>
        /// <param name="falsePositive"></param>
        /// <param name="trueNegatives"></param>
        /// <returns></returns>
        public static PrecisionRecallCalculator<bool> Generate(int truePositives, int falseNegatives, int falsePositive, int trueNegatives)
        {
            PrecisionRecallCalculator<bool> calculator = new PrecisionRecallCalculator<bool>();
            calculator.AddTruePositives(truePositives);
            calculator.AddFalsePositives(falsePositive);
            calculator.AddTrueNegatives(trueNegatives);
            calculator.AddFalseNegatives(falseNegatives);
            return calculator;
        }

        private static void AddFalseNegatives(this PrecisionRecallCalculator<bool> calculator, int total)
        {
            for (int i = 0; i < total; i++)
            {
                calculator.Add(true, false);
            }
        }

        private static void AddFalsePositives(this PrecisionRecallCalculator<bool> calculator, int total)
        {
            for (int i = 0; i < total; i++)
            {
                calculator.Add(false, true);
            }
        }

        private static void AddTrueNegatives(this PrecisionRecallCalculator<bool> calculator, int total)
        {
            for (int i = 0; i < total; i++)
            {
                calculator.Add(false, false);
            }
        }

        private static void AddTruePositives(this PrecisionRecallCalculator<bool> calculator, int total)
        {
            for (int i = 0; i < total; i++)
            {
                calculator.Add(true, true);
            }
        }
    }
}
