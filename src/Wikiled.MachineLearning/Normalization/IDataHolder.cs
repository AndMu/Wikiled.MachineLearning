namespace Wikiled.MachineLearning.Normalization
{
    public interface IDataHolder
    {
        ColumnType Type { get; }

        string Text { get; }

        double Value { get; }
    }
}