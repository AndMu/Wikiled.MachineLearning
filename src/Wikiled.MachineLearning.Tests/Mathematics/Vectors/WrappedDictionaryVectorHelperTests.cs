using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors
{
    [TestFixture]
    public class WrappedDictionaryVectorHelperTests
    {
        private DictionaryVectorHelper helper;

        [SetUp]
        public void Setup()
        {
            helper = new DictionaryVectorHelper();
            helper.Add("One", 1);
            helper.Add("Two", 2);
            helper.Add("Three", 3);
        }

        [Test]
        public void GetFullVectorEmpty()
        {
            WrappedDictionaryVectorHelper wrapped = new WrappedDictionaryVectorHelper(helper.GetFullVector());
            var vector = wrapped.GetFullVector();
            Assert.AreEqual(3, vector.Length);
            Assert.AreEqual(0, wrapped.Total);
            Assert.AreEqual(0, vector.OriginalCells.Length);
        }

        [Test]
        public void GetFullVector()
        {
            WrappedDictionaryVectorHelper wrapped = new WrappedDictionaryVectorHelper(helper.GetFullVector());
            wrapped.Add("one", 20);
            var vector = wrapped.GetFullVector();
            Assert.AreEqual(3, vector.Length);
            Assert.AreEqual(20, wrapped.Total);
            Assert.AreEqual(1, vector.OriginalCells.Length);
            Assert.AreEqual(20, vector.OriginalCells[0].X);
        }
    }
}
