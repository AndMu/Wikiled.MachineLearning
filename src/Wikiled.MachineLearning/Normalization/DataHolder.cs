
using System;

namespace Wikiled.MachineLearning.Normalization
{
    public class DataHolder : IDataHolder
    {
        public DataHolder(string text, ColumnType type)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(text));
            }

            Text = text;
            Type = type;
        }

        public ColumnType Type { get; }

        public string Text { get; }

        public double Value => double.Parse(Text);
    }
}
