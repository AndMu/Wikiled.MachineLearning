using System;
using System.Collections.Generic;
using System.Linq;

namespace Wikiled.MachineLearning.Normalization
{
    public class L2Normalize : BaseNormalize
    {
        public L2Normalize(IEnumerable<double> source)
            : base(source)
        {
        }

        public override NormalizationType Type => NormalizationType.L2;

        protected override double CalculateCoef()
        {
            return Math.Sqrt(Source.Sum(item => item * item));
        }
    }
}
