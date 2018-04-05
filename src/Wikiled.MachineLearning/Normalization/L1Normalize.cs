using System;
using System.Collections.Generic;
using System.Linq;

namespace Wikiled.MachineLearning.Normalization
{
    public class L1Normalize : BaseNormalize
    {
        public L1Normalize(IEnumerable<double> source)
            : base(source)
        {
        }

        public override NormalizationType Type => NormalizationType.L1;

        protected override double CalculateCoef()
        {
            return Source.Sum(item => Math.Abs(item));
        }
    }
}
