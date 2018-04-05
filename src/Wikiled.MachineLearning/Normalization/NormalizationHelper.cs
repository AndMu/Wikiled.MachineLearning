using System.Collections.Generic;

namespace Wikiled.MachineLearning.Normalization
{
    public static class NormalizationHelper
    {
        public static INormalize Normalize(this IEnumerable<double> source, NormalizationType type)
        {
            switch (type)
            {
                case NormalizationType.L1:
                    return new L1Normalize(source);
                case NormalizationType.L2:
                    return new L2Normalize(source);
                case NormalizationType.Elastic:
                    return new ElasticNormalization(source);
                default:
                    return new NullNormalization(source);
            }
        }
    }
}
