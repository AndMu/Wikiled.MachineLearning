using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Wikiled.MachineLearning.Mathematics;

namespace Wikiled.MachineLearning.Tests.Mathematics
{
    [TestFixture]
    public class RatingExtensionTests
    {
        [Test]
        public void GetPercentage()
        {
            ClassicAssert.AreEqual(75, 0.5.GetPercentage());
            ClassicAssert.AreEqual(100, 1.0.GetPercentage());
            ClassicAssert.AreEqual(50, 0.0.GetPercentage());
            ClassicAssert.AreEqual(25, (-0.50).GetPercentage());
            ClassicAssert.AreEqual(0, (-1.0).GetPercentage());
        }

        [Test]
        public void GetPercentageOutOfRange1()
        {
            ClassicAssert.Throws<ArgumentOutOfRangeException>(() => 3.0.GetPercentage());
        }

        [Test]
        public void GetPercentageOutOfRange2()
        {
            ClassicAssert.Throws<ArgumentOutOfRangeException>(() => (-3.0).GetPercentage());
        }

        [Test]
        public void GetStars()
        {
            ClassicAssert.AreEqual(0.5, 0.6.GetStars());
            ClassicAssert.AreEqual(0, 0.1.GetStars());
            ClassicAssert.AreEqual(1, 1.1.GetStars());
            ClassicAssert.AreEqual(2, 1.9.GetStars());
            ClassicAssert.AreEqual(1.5, 1.74.GetStars());
            ClassicAssert.AreEqual(1, 1.24.GetStars());
            ClassicAssert.AreEqual(1.5, 1.26.GetStars());
        }
    }
}
