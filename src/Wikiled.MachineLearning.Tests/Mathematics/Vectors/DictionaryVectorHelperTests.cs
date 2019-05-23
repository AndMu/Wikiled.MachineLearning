using System;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors
{
    [TestFixture]
    public class DictionaryVectorHelperTests
    {
        private OneHotEncoder instance;

        [SetUp]
        public void SetUp()
        {
            instance = CreateDictionaryVectorHelper();
        }

        [Test]
        public void Arguments()
        {
            Assert.Throws<ArgumentException>(() => instance.AddWord(null));
            Assert.Throws<ArgumentNullException>(() => instance.GetFullVector(null));
        }

        [Test]
        public void SimpleTestCase()
        {
            instance.AddWord("One");
            instance.AddWord("Two");
            instance.AddWord("Three");
            var vector = instance.GetFullVector("One", "three");
            Assert.AreEqual(3, vector.Length);
            Assert.AreEqual(2, vector.Cells.Length);
        }

        private OneHotEncoder CreateDictionaryVectorHelper()
        {
            return new OneHotEncoder();
        }
    }
}