using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Statistics;

namespace Wikiled.MachineLearning.Tests.Mathematics.Statistics
{
    [TestFixture]
    public class DataPointsTransformTests
    {
        [Test]
        public void Test()
        {
            int totalChanged = 0;
            DataPointsTransform transform = new DataPointsTransform(new double[] {1, 2, 3, 4, 5, 6});
            transform.WindowSize = 1;
            transform.Changed += (sender, args) => totalChanged++;
            Assert.AreEqual(6, transform.Data.Length);
            Assert.AreEqual(1, transform.WindowSize);
            Assert.AreEqual(1, transform.Minimum);
            Assert.AreEqual(6, transform.Maximum);
            Assert.AreEqual(DataProcessingType.None, transform.Normalization);
            Assert.AreEqual(6, transform.Total);
            Assert.AreEqual(0, totalChanged);

            transform.Normalization = DataProcessingType.MovingAverage;
            Assert.AreEqual(6, transform.Data.Length);
            Assert.AreEqual(1, transform.WindowSize);
            Assert.AreEqual(1, transform.Minimum);
            Assert.AreEqual(6, transform.Maximum);
            Assert.AreEqual(DataProcessingType.MovingAverage, transform.Normalization);
            Assert.AreEqual(6, transform.Total);
            Assert.AreEqual(1, totalChanged);

            transform.WindowSize = 3;
            Assert.AreEqual(4, transform.Data.Length);
            Assert.AreEqual(3, transform.WindowSize);
            Assert.AreEqual(2, transform.Minimum);
            Assert.AreEqual(5, transform.Maximum);
            Assert.AreEqual(DataProcessingType.MovingAverage, transform.Normalization);
            Assert.AreEqual(4, transform.Total);
            Assert.AreEqual(2, totalChanged);
        }
    }
}
