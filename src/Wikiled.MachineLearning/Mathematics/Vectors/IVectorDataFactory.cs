using Wikiled.MachineLearning.Normalization;
using NormalizationType = Wikiled.MachineLearning.Normalization.NormalizationType;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public interface IVectorDataFactory
    {
        VectorData CreateSimple(NormalizationType normalizationType, params double[] cells);

        VectorData CreateSimple(NormalizationType normalizationType, params ICell[] cells);
    }
}