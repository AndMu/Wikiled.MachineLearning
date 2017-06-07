using NUnit.Framework;
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
            Assert.AreEqual(2, clusters.Length);
            
            Assert.AreEqual(3, clusters[0].Length);
            Assert.AreEqual(0, clusters[0].StartIndex);
            Assert.AreEqual(2, clusters[0].EndIndex);
            Assert.AreEqual(3, clusters[0].Peak);

            Assert.AreEqual(2, clusters[1].Length);
            Assert.AreEqual(4, clusters[1].StartIndex);
            Assert.AreEqual(5, clusters[1].EndIndex);
            Assert.AreEqual(3, clusters[1].Peak);
        }

        [Test]
        public void GetRegionsMinSize()
        {
            ClusterRegion[] clusters = ClusterFlow.GetRegions(new double[] { 1, 2, 3, 0, 2, 3 }, 3);
            Assert.AreEqual(1, clusters.Length);

            Assert.AreEqual(3, clusters[0].Length);
            Assert.AreEqual(0, clusters[0].StartIndex);
            Assert.AreEqual(2, clusters[0].EndIndex);
            Assert.AreEqual(3, clusters[0].Peak);
        }

        [Test]
        public void GetRegionsNegative()
        {
            ClusterRegion[] clusters = ClusterFlow.GetRegions(new double[] { 1, 2, 3, -1, -3, -4 }, 1);
            Assert.AreEqual(2, clusters.Length);

            Assert.AreEqual(3, clusters[0].Length);
            Assert.AreEqual(0, clusters[0].StartIndex);
            Assert.AreEqual(2, clusters[0].EndIndex);
            Assert.AreEqual(3, clusters[0].Peak);

            Assert.AreEqual(3, clusters[1].Length);
            Assert.AreEqual(3, clusters[1].StartIndex);
            Assert.AreEqual(5, clusters[1].EndIndex);
            Assert.AreEqual(-4, clusters[1].Peak);
        }
    }
}
