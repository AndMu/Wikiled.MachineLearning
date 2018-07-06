using System;

namespace Wikiled.MachineLearning.Mathematics.Statistics
{
    public class AttachedDataPointsTransform : DataPointsTransform
    {
        private readonly DataPointsTransform parent;

        public AttachedDataPointsTransform(DataPointsTransform parent, double[] originalData)
            : base(originalData)
        {
            this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
            parent.Changed += ParentChanged;
        }

        public override DataProcessingType Normalization
        {
            get => parent.Normalization;
            set => parent.Normalization = value;
        }

        public override int WindowSize
        {
            get => parent.WindowSize;
            set => parent.WindowSize = value;
        }

        private void ParentChanged(object sender, EventArgs e)
        {
            DataChanged();
        }
    }
}
