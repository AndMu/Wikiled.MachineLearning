using System.Collections.Generic;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public interface IJsonVectorSerialization
    {
        void Serialize(IEnumerable<VectorData> data);
    }
}