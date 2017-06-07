using System;
using System.Linq;
using Wikiled.Arff.Normalization;
using Wikiled.Core.Utility.Arguments;

namespace Wikiled.MachineLearning.Mathematics.Statistics
{
    public class DataPointsTransform
    {
        public event EventHandler<EventArgs> Changed;

        private DataProcessingType normalization;
        private readonly double[] originalData;
        private double[] data;
        private int windowSize = 3;

        public DataPointsTransform(double[] originalData, DataProcessingType defaultNormalization = DataProcessingType.None)
        {
            Guard.NotEmpty(() => originalData, originalData);
            this.originalData = originalData;
            normalization = defaultNormalization;
        }

        protected void DataChanged()
        {
            data = null;
            Changed?.Invoke(this, EventArgs.Empty);
        }

        private double[] Calculate()
        {
            switch (Normalization)
            {
                case DataProcessingType.MovingAverage:
                    return originalData.MovingAverage(WindowSize).ToArray();
                case DataProcessingType.ExpMovingAverage:
                    return originalData.ExponentialMovingAverage(WindowSize).ToArray();
                case DataProcessingType.WeightedMovingAverage:
                    return originalData.WeightedMovingAverage(WindowSize).ToArray();
                default:
                    return originalData;
            }
        }

        public double[] Data
        {
            get { return data ?? (data = Calculate()); }
        }

        public double Minimum
        {
            get { return Data.Min(item => item); }
        }

        public double Maximum
        {
            get { return Data.Max(item => item); }
        }

        public int MaxWindow
        {
            get { return originalData.Length; }
        }

        public int Total
        {
            get { return Data.Length; }
        }

        public virtual int WindowSize
        {
            get { return windowSize; }
            set
            {
                if (windowSize == value)
                {
                    return;
                }

                windowSize = value;
                DataChanged();
            }
        }

        public virtual DataProcessingType Normalization
        {
            get { return normalization; }
            set
            {
                normalization = value;
                DataChanged();
            }
        }
    }
}
