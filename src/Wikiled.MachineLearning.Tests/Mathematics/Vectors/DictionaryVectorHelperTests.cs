using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors
{
    [TestFixture]
    public class DictionaryVectorHelperTests
    {
        [Test]
        public void GetFullVector()
        {
            DictionaryVectorHelper helper = new DictionaryVectorHelper();
            helper.Add("Test1", 1);
            helper.Add("Test2", 20);
            var vector = helper.GetFullVector();
            Assert.AreEqual(21, helper.Total);
            Assert.AreEqual(2, vector.Length);
            Assert.AreEqual(20, vector.ValueCellsX[0]);
            Assert.AreEqual(1, vector.ValueCellsX[1]);
        }
    }
}
