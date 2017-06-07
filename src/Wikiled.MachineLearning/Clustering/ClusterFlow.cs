using System.Collections.Generic;
using System.Linq;

namespace Wikiled.MachineLearning.Clustering
{
    public static class ClusterFlow
    {
        public static ClusterRegion[] GetRegions(double[] data, int minClusterSize)
        {
            List<ClusterRegion> regions = new List<ClusterRegion>();
            ClusterRegion currentRegion = null;

            for (int i = 0; i < data.Length; i++)
            {
                if (currentRegion != null)
                {
                    bool isPositive = data[i] > 0;
                    if (data[i] == 0 ||
                        isPositive != currentRegion.IsPositive)
                    {
                        currentRegion = null;
                    }
                }

                if (data[i] == 0)
                {
                    continue;
                }

                if (currentRegion == null)
                {
                    currentRegion = new ClusterRegion(i);
                    regions.Add(currentRegion);
                }

                currentRegion.Add(data[i]);
            }

            return regions.Where(item => item.Length >= minClusterSize).ToArray();
        }
    }
}
