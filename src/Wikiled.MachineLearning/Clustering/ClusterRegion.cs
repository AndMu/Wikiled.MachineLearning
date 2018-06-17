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

        public int EndIndex => StartIndex + Length - 1;

        public bool IsPositive => Peak > 0;

        public IList<double> Items => items;

        public int Length => items.Count;

        public double Peak { get; private set; }

        public int StartIndex { get; }

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