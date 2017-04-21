﻿namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class SimpleCell : ICell
    {
        public SimpleCell(string name, double value)
        {
            Value = value;
            Name = name;
        }

        public object Clone()
        {
            return new SimpleCell(Name, Value);
        }

        public double Value { get; private set; }

        public string Name { get; private set; }
    }
}
