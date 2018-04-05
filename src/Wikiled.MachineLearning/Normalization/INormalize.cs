using System.Collections.Generic;

namespace Wikiled.MachineLearning.Normalization
{
    public interface INormalize
    {
        double Coeficient { get; }

        IEnumerable<double> GetNormalized { get; }

        NormalizationType Type { get; }
    }
}
