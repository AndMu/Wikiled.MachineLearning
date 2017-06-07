using System;
using Wikiled.Core.Utility.Arguments;

namespace Wikiled.MachineLearning.Mathematics.Statistics
{
    public class AttachedDataPointsTransform : DataPointsTransform
    {
        private readonly DataPointsTransform parent;

        public AttachedDataPointsTransform(DataPointsTransform parent, double[] originalData)
            : base(originalData)
        {
            Guard.NotNull(() => parent, parent);
            this.parent = parent;
            parent.Changed += parent_Changed;
        }

        void parent_Changed(object sender, EventArgs e)
        {
            DataChanged();
        }

        public override int WindowSize
        {
            get
            {
                return parent.WindowSize;
            }
            set
            {
                parent.WindowSize = value;
            }
        }

        public override DataProcessingType Normalization
        {
            get
            {
                return parent.Normalization;
            }
            set
            {
                parent.Normalization = value;
            }
        }
    }
}
