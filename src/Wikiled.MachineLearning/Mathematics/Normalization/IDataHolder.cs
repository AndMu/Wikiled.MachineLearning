namespace Wikiled.MachineLearning.Mathematics.Normalization
{
    public interface IDataHolder
    {
        ColumnType Type { get; }

        string Text { get; }

        double Value { get; }
    }
}