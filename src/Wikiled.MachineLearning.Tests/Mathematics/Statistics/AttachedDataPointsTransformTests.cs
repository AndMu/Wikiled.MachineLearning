using NUnit.Framework;
using NUnit.Framework.Legacy;
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
            ClassicAssert.AreEqual(6, child.Data.Length);
            ClassicAssert.AreEqual(1, child.WindowSize);
            ClassicAssert.AreEqual(1, child.Minimum);
            ClassicAssert.AreEqual(6, child.Maximum);
            ClassicAssert.AreEqual(DataProcessingType.None, transform.Normalization);
            ClassicAssert.AreEqual(6, child.Total);
            ClassicAssert.AreEqual(0, totalChanged);

            transform.Normalization = DataProcessingType.MovingAverage;
            ClassicAssert.AreEqual(6, child.Data.Length);
            ClassicAssert.AreEqual(1, child.WindowSize);
            ClassicAssert.AreEqual(1, child.Minimum);
            ClassicAssert.AreEqual(6, child.Maximum);
            ClassicAssert.AreEqual(DataProcessingType.MovingAverage, child.Normalization);
            ClassicAssert.AreEqual(6, child.Total);
            ClassicAssert.AreEqual(1, totalChanged);

            transform.WindowSize = 3;
            ClassicAssert.AreEqual(4, child.Data.Length);
            ClassicAssert.AreEqual(3, child.WindowSize);
            ClassicAssert.AreEqual(2, child.Minimum);
            ClassicAssert.AreEqual(5, child.Maximum);
            ClassicAssert.AreEqual(DataProcessingType.MovingAverage, child.Normalization);
            ClassicAssert.AreEqual(4, child.Total);
            ClassicAssert.AreEqual(2, totalChanged);
        }
    }
}
