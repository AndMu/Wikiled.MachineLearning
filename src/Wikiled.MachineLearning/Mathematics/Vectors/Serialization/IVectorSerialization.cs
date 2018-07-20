using System.Collections.Generic;

namespace Wikiled.MachineLearning.Mathematics.Vectors.Serialization
{
    public interface IVectorSerialization
    {
        void Serialize(IEnumerable<VectorData> data);

        IEnumerable<VectorData> Deserialize();
    }
}