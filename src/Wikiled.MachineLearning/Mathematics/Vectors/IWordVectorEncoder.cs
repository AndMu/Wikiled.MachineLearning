namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public interface IWordVectorEncoder
    {
        void AddWords(params string[] words);

        void AddWord(string word);

        VectorData GetFullVector(params string[] words);

        double Total { get; }
    }
}