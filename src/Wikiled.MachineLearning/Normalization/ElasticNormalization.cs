using System.Collections.Generic;

namespace Wikiled.MachineLearning.Normalization
{
    public class ElasticNormalization : BaseNormalize
    {
        private readonly L1Normalize l1;

        private readonly L2Normalize l2;

        public ElasticNormalization(IEnumerable<double> source)
            : base(source)
        {
            l1 = new L1Normalize(source);
            l2 = new L2Normalize(source);
        }

        public override NormalizationType Type => NormalizationType.Elastic;

        protected override double CalculateCoef()
        {
            return 0.15 * l1.Coeficient + 0.85 * l2.Coeficient;
        }
    }
}
