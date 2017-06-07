using System;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics;

namespace Wikiled.MachineLearning.Tests.Mathematics
{
    [TestFixture]
    public class RatingExtensionTests
    {
        [Test]
        public void GetPercentage()
        {
            Assert.AreEqual(75, 0.5.GetPercentage());
            Assert.AreEqual(100, 1.0.GetPercentage());
            Assert.AreEqual(50, 0.0.GetPercentage());
            Assert.AreEqual(25, (-0.50).GetPercentage());
            Assert.AreEqual(0, (-1.0).GetPercentage());
        }

        [Test]
        public void GetPercentageOutOfRange1()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 3.0.GetPercentage());
        }

        [Test]
        public void GetPercentageOutOfRange2()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => (-3.0).GetPercentage());
        }

        [Test]
        public void GetStars()
        {
            Assert.AreEqual(0.5, 0.6.GetStars());
            Assert.AreEqual(0, 0.1.GetStars());
            Assert.AreEqual(1, 1.1.GetStars());
            Assert.AreEqual(2, 1.9.GetStars());
            Assert.AreEqual(1.5, 1.74.GetStars());
            Assert.AreEqual(1, 1.24.GetStars());
            Assert.AreEqual(1.5, 1.26.GetStars());
        }
    }
}
