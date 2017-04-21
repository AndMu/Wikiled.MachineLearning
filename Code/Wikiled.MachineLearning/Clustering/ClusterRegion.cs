using System;
using System.Collections.Generic;

namespace Wikiled.MachineLearning.Clustering
{
    public class ClusterRegion
    {
        private readonly List<double> items = new List<double>();

        public ClusterRegion(int start)
        {
            StartIndex = start;
        }

        public int EndIndex
        {
            get { return StartIndex + Length - 1; }
        }

        public bool IsPositive
        {
            get { return Peak > 0; }
        }

        public IList<double> Items
        {
            get { return items; }
        }

        public int Length
        {
            get { return items.Count; }
        }

        public double Peak { get; private set; }

        public int StartIndex { get; private set; }

        public void Add(double item)
        {
            items.Add(item);
            if (Math.Abs(Peak) < Math.Abs(item))
            {
                Peak = item;
            }
        }
    }
}