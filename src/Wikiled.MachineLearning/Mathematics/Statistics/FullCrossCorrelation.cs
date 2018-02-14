using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wikiled.Common.Extensions;

namespace Wikiled.MachineLearning.Mathematics.Statistics
{
    public class FullCrossCorrelation
    {
        private readonly bool isInverted;

        public FullCrossCorrelation(bool isInverted, double[] signal, double[] pattern)
        {
            this.isInverted = isInverted;
            Signal = signal;
            Pattern = pattern;
        }

        public double Auto { get; private set; }

        public double[] Correlations { get; private set; }

        public int Offset { get; private set; }

        public double[] Pattern { get; }

        public CorrelationResult Result { get; private set; }

        public double[] Signal { get; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("Offset: {0}\r\n", Offset);
            result.AppendFormat("Auto: {0}\r\n", Auto);
            result.AppendFormat("Correlations: {0}\r\n", Correlations.Select(item => item.ToString()).AccumulateItems(","));
            result.Append(Result);
            return result.ToString();
        }

        public void Calculate(int? take = null)
        {
            double[] results;
            alglib.corrr1d(Signal, Signal.Length, Pattern, Pattern.Length, out results);

            double max = 0;
            var i = 0;

            int offset = !take.HasValue || take.Value > Signal.Length ? Signal.Length : take.Value;

            List<double> addedResults = new List<double>(results.Take(offset));

            addedResults.AddRange(results.Skip(results.Length - offset));
            Correlations = addedResults.ToArray();

            // Find the maximum cross correlation value and its index
            foreach (var result in Correlations)
            {
                bool bigger = isInverted ? result < max : result > max;
                if (bigger)
                {
                    Offset = i;
                    max = result;
                }

                i++;
            }

            if (Offset >= offset)
            {
                Offset = Offset - 2 * offset;
            }

            Result = SimpleCrossCorrelation.Calculate(Pattern, Signal, Offset);
            Auto = SimpleCrossCorrelation.Calculate(Signal, Signal, Offset).Result;
        }
    }
}
