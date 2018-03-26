namespace Wikiled.MachineLearning.Mathematics.Normalization
{
    public class DoubleDataHolder : IDataHolder
    {
        public DoubleDataHolder(double value)
        {
            Value = value;
            Text = value.ToString();
        }

        public ColumnType Type => ColumnType.Numeric;

        public string Text { get; }

        public double Value { get; }
    }
}
