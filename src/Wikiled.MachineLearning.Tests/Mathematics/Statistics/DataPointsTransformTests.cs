using NUnit.Framework;
using NUnit.Framework.Legacy;
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
            ClassicAssert.AreEqual(6, transform.Data.Length);
            ClassicAssert.AreEqual(1, transform.WindowSize);
            ClassicAssert.AreEqual(1, transform.Minimum);
            ClassicAssert.AreEqual(6, transform.Maximum);
            ClassicAssert.AreEqual(DataProcessingType.None, transform.Normalization);
            ClassicAssert.AreEqual(6, transform.Total);
            ClassicAssert.AreEqual(0, totalChanged);

            transform.Normalization = DataProcessingType.MovingAverage;
            ClassicAssert.AreEqual(6, transform.Data.Length);
            ClassicAssert.AreEqual(1, transform.WindowSize);
            ClassicAssert.AreEqual(1, transform.Minimum);
            ClassicAssert.AreEqual(6, transform.Maximum);
            ClassicAssert.AreEqual(DataProcessingType.MovingAverage, transform.Normalization);
            ClassicAssert.AreEqual(6, transform.Total);
            ClassicAssert.AreEqual(1, totalChanged);

            transform.WindowSize = 3;
            ClassicAssert.AreEqual(4, transform.Data.Length);
            ClassicAssert.AreEqual(3, transform.WindowSize);
            ClassicAssert.AreEqual(2, transform.Minimum);
            ClassicAssert.AreEqual(5, transform.Maximum);
            ClassicAssert.AreEqual(DataProcessingType.MovingAverage, transform.Normalization);
            ClassicAssert.AreEqual(4, transform.Total);
            ClassicAssert.AreEqual(2, totalChanged);
        }
    }
}
