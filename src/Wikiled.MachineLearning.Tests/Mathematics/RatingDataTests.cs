using System;
using NUnit.Framework;
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
            Assert.IsFalse(data.IsStrong);
            data.AddPositive(2);
            Assert.IsFalse(data.IsStrong);
            data.AddPositive(2);
            Assert.IsTrue(data.IsStrong);

            data.AddNegative(2);
            Assert.IsFalse(data.IsStrong);
            data.AddPositive(2);
            Assert.IsTrue(data.IsStrong);
        }

        [Test]
        public void IsStrongNegative()
        {
            Assert.IsFalse(data.IsStrong);
            data.AddNegative(2);
            Assert.IsFalse(data.IsStrong);
            data.AddNegative(2);
            Assert.IsTrue(data.IsStrong);

            data.AddPositive(2);
            Assert.IsFalse(data.IsStrong);
            data.AddNegative(2);
            Assert.IsTrue(data.IsStrong);
        }

        [Test]
        public void IsStrongNegative2()
        {
            Assert.IsFalse(data.IsStrong);
            data.AddSetiment(-2);
            Assert.IsFalse(data.IsStrong);
            data.AddSetiment(-2);
            Assert.IsTrue(data.IsStrong);
            Assert.AreEqual(-1, data.RawRating);

            data.AddSetiment(2);
            Assert.IsFalse(data.IsStrong);
            data.AddSetiment(-2);
            Assert.IsTrue(data.IsStrong);
        }

        [Test]
        public void TestArguments()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => data.AddPositive(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => data.AddNegative(-1));
        }
    }
}
