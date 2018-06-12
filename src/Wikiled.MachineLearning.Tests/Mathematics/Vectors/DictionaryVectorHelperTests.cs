using System;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors
{
    [TestFixture]
    public class DictionaryVectorHelperTests
    {
        private DictionaryVectorHelper instance;

        [SetUp]
        public void SetUp()
        {
            instance = CreateDictionaryVectorHelper();
        }

        [Test]
        public void Arguments()
        {
            Assert.Throws<ArgumentNullException>(() => instance.AddToDictionary(null));
            Assert.Throws<ArgumentNullException>(() => instance.GetFullVector(null));
        }

        [Test]
        public void SimpleTestCase()
        {
            instance.AddToDictionary("One");
            instance.AddToDictionary("Two");
            instance.AddToDictionary("Three");
            var vector = instance.GetFullVector("One", "three");
            Assert.AreEqual(3, vector.Length);
            Assert.AreEqual(2, vector.Cells.Length);
        }

        private DictionaryVectorHelper CreateDictionaryVectorHelper()
        {
            return new DictionaryVectorHelper();
        }
    }
}