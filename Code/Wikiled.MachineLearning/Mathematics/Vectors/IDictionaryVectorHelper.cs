namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public interface IDictionaryVectorHelper
    {
        void Add(string name, double value);

        VectorData GetFullVector()
            ;
        double Total { get; }
    }
}