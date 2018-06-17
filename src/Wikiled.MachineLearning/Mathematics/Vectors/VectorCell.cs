using System;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class VectorCell : ICloneable
    {
        private double? xAdjusted;

        public VectorCell()
        {
            Theta = 1;
        }

        public VectorCell(int index, ICell textCell, double theta)
        {
            Theta = theta;
            Index = index;
            Data = textCell;
        }

        public double Calculated => X * Theta;

        public ICell Data { get; }

        public int Index { get; }

        public double Theta { get; }

        public double X { get => xAdjusted ?? Data.Value; set => xAdjusted = value; }

        public override string ToString()
        {
            return string.Format("{2}:<{0}>:{1}:{3}", Data.Name, X, Index, Calculated);
        }

        public object Clone()
        {
            var cloned = new VectorCell(Index, (ICell)Data.Clone(), Theta);
            cloned.xAdjusted = xAdjusted;
            return cloned;
        }
    }
}
