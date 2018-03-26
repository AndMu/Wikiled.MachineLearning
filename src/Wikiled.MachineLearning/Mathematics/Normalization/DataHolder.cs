using Wikiled.Common.Arguments;

namespace Wikiled.MachineLearning.Mathematics.Normalization
{
    public class DataHolder : IDataHolder
    {
        public DataHolder(string text, ColumnType type)
        {
            Guard.NotNullOrEmpty(() => text, text);
            Text = text;
            Type = type;
        }

        public ColumnType Type { get; }

        public string Text { get; }

        public double Value => double.Parse(Text);
    }
}
