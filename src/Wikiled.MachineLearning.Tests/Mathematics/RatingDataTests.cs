using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Wikiled.MachineLearning.Mathematics;

namespace Wikiled.MachineLearning.Tests.Mathematics
{
    [TestFixture]
    public class RatingDataTests
    {
        private RatingData data;

        [SetUp]
        public void Setup()
        {
            data = new RatingData();
        }

        [Test]
        public void IsStrongPositive()
        {
            ClassicAssert.IsFalse(data.IsStrong);
            data.AddPositive(2);
            ClassicAssert.IsFalse(data.IsStrong);
            data.AddPositive(2);
            ClassicAssert.IsTrue(data.IsStrong);

            data.AddNegative(2);
            ClassicAssert.IsFalse(data.IsStrong);
            data.AddPositive(2);
            ClassicAssert.IsTrue(data.IsStrong);
        }

        [Test]
        public void IsStrongNegative()
        {
            ClassicAssert.IsFalse(data.IsStrong);
            data.AddNegative(2);
            ClassicAssert.IsFalse(data.IsStrong);
            data.AddNegative(2);
            ClassicAssert.IsTrue(data.IsStrong);

            data.AddPositive(2);
            ClassicAssert.IsFalse(data.IsStrong);
            data.AddNegative(2);
            ClassicAssert.IsTrue(data.IsStrong);
        }

        [Test]
        public void IsStrongNegative2()
        {
            ClassicAssert.IsFalse(data.IsStrong);
            data.AddSetiment(-2);
            ClassicAssert.IsFalse(data.IsStrong);
            data.AddSetiment(-2);
            ClassicAssert.IsTrue(data.IsStrong);
            ClassicAssert.AreEqual(-1, data.RawRating);

            data.AddSetiment(2);
            ClassicAssert.IsFalse(data.IsStrong);
            data.AddSetiment(-2);
            ClassicAssert.IsTrue(data.IsStrong);
        }

        [Test]
        public void TestArguments()
        {
            ClassicAssert.Throws<ArgumentOutOfRangeException>(() => data.AddPositive(-1));
            ClassicAssert.Throws<ArgumentOutOfRangeException>(() => data.AddNegative(-1));
        }
    }
}
