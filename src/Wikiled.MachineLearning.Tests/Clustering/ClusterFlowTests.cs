using NUnit.Framework;
using NUnit.Framework.Legacy;
using Wikiled.MachineLearning.Clustering;

namespace Wikiled.MachineLearning.Tests.Clustering
{
    [TestFixture]
    public class ClusterFlowTests
    {
        [Test]
        public void GetRegionsSeparatedByZero()
        {
            ClusterRegion[] clusters = ClusterFlow.GetRegions(new double[] { 1, 2, 3, 0, 2, 3 }, 1);
            ClassicAssert.AreEqual(2, clusters.Length);
            
            ClassicAssert.AreEqual(3, clusters[0].Length);
            ClassicAssert.AreEqual(0, clusters[0].StartIndex);
            ClassicAssert.AreEqual(2, clusters[0].EndIndex);
            ClassicAssert.AreEqual(3, clusters[0].Peak);

            ClassicAssert.AreEqual(2, clusters[1].Length);
            ClassicAssert.AreEqual(4, clusters[1].StartIndex);
            ClassicAssert.AreEqual(5, clusters[1].EndIndex);
            ClassicAssert.AreEqual(3, clusters[1].Peak);
        }

        [Test]
        public void GetRegionsMinSize()
        {
            ClusterRegion[] clusters = ClusterFlow.GetRegions(new double[] { 1, 2, 3, 0, 2, 3 }, 3);
            ClassicAssert.AreEqual(1, clusters.Length);

            ClassicAssert.AreEqual(3, clusters[0].Length);
            ClassicAssert.AreEqual(0, clusters[0].StartIndex);
            ClassicAssert.AreEqual(2, clusters[0].EndIndex);
            ClassicAssert.AreEqual(3, clusters[0].Peak);
        }

        [Test]
        public void GetRegionsNegative()
        {
            ClusterRegion[] clusters = ClusterFlow.GetRegions(new double[] { 1, 2, 3, -1, -3, -4 }, 1);
            ClassicAssert.AreEqual(2, clusters.Length);

            ClassicAssert.AreEqual(3, clusters[0].Length);
            ClassicAssert.AreEqual(0, clusters[0].StartIndex);
            ClassicAssert.AreEqual(2, clusters[0].EndIndex);
            ClassicAssert.AreEqual(3, clusters[0].Peak);

            ClassicAssert.AreEqual(3, clusters[1].Length);
            ClassicAssert.AreEqual(3, clusters[1].StartIndex);
            ClassicAssert.AreEqual(5, clusters[1].EndIndex);
            ClassicAssert.AreEqual(-4, clusters[1].Peak);
        }
    }
}
