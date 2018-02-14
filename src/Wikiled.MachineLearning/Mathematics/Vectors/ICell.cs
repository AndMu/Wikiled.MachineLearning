using System;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public interface ICell : ICloneable
    {
        double Value { get; }

        string Name { get; }
    }
}
