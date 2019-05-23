using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors
{
    [TestFixture]
    public class OneHotEncoderTests
    {
        private OneHotEncoder instance;
        [SetUp]
        public void SetUp()
        {
            instance = new OneHotEncoder();
        }

        [Test]
        public void AddToDictionary()
        {
            instance.AddWords("Test", "Two", "one");
            Assert.AreEqual(3, instance.Total);
            var vector = instance.GetFullVector("Two", "ONe", "Unknown");
            Assert.AreEqual(3, vector.Length);
            Assert.AreEqual(new double[] { 0, 1, 1 }, vector.FullValues);
        }
    }
}
