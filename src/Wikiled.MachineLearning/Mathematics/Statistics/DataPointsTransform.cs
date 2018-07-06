using System;
using System.Linq;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Mathematics.Statistics
{
    public class DataPointsTransform
    {
        public event EventHandler<EventArgs> Changed;

        private readonly double[] originalData;

        private double[] data;

        private DataProcessingType normalization;

        private int windowSize = 3;

        public DataPointsTransform(double[] originalData, DataProcessingType defaultNormalization = DataProcessingType.None)
        {
            if (originalData.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(originalData));
            }

            this.originalData = originalData;
            normalization = defaultNormalization;
        }

        public virtual DataProcessingType Normalization
        {
            get => normalization;
            set
            {
                normalization = value;
                DataChanged();
            }
        }

        public virtual int WindowSize
        {
            get => windowSize;
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

        public double[] Data => data ?? (data = Calculate());

        public double Maximum
        {
            get
            {
                return Data.Max(item => item);
            }
        }

        public int MaxWindow => originalData.Length;

        public double Minimum
        {
            get
            {
                return Data.Min(item => item);
            }
        }

        public int Total => Data.Length;

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
    }
}
