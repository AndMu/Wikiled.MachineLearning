using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Statistics;

namespace Wikiled.MachineLearning.Tests.Mathematics.Statistics
{
    [TestFixture]
    public class AttachedDataPointsTransformTests
    {
        [Test]
        public void Test()
        {
            int totalChanged = 0;
            DataPointsTransform transform = new DataPointsTransform(new double[] { 2, 2, 2, 3, 3, 3 });
            transform.WindowSize = 1;
            AttachedDataPointsTransform child = new AttachedDataPointsTransform(transform, new double[] { 1, 2, 3, 4, 5, 6 });
            child.Changed += (sender, args) => totalChanged++;
            Assert.AreEqual(6, child.Data.Length);
            Assert.AreEqual(1, child.WindowSize);
            Assert.AreEqual(1, child.Minimum);
            Assert.AreEqual(6, child.Maximum);
            Assert.AreEqual(DataProcessingType.None, transform.Normalization);
            Assert.AreEqual(6, child.Total);
            Assert.AreEqual(0, totalChanged);

            transform.Normalization = DataProcessingType.MovingAverage;
            Assert.AreEqual(6, child.Data.Length);
            Assert.AreEqual(1, child.WindowSize);
            Assert.AreEqual(1, child.Minimum);
            Assert.AreEqual(6, child.Maximum);
            Assert.AreEqual(DataProcessingType.MovingAverage, child.Normalization);
            Assert.AreEqual(6, child.Total);
            Assert.AreEqual(1, totalChanged);

            transform.WindowSize = 3;
            Assert.AreEqual(4, child.Data.Length);
            Assert.AreEqual(3, child.WindowSize);
            Assert.AreEqual(2, child.Minimum);
            Assert.AreEqual(5, child.Maximum);
            Assert.AreEqual(DataProcessingType.MovingAverage, child.Normalization);
            Assert.AreEqual(4, child.Total);
            Assert.AreEqual(2, totalChanged);
        }
    }
}
