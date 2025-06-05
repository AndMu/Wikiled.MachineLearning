using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Wikiled.MachineLearning.Mathematics;

namespace Wikiled.MachineLearning.Tests.Mathematics
{
    [TestFixture]
    public class CoefficientsTests
    {
        [Test]
        public void WebJaccard()
        {
            var total = Coefficients.WebJaccard(10, 10, 3);
            ClassicAssert.AreEqual(0.18, Math.Round(total, 2));
        }

        [Test]
        public void WebPMI()
        {
            var total = Coefficients.WebPMI(6, 100, 3);
            ClassicAssert.AreEqual(-5.14, Math.Round(total, 2));
        }
    }
}
