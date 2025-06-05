using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
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
        public void Complex()
        {
            instance.AddWords("Test", "Two", "one");
            ClassicAssert.AreEqual(3, instance.Total);
            var vector = instance.GetFullVector("Two", "ONe", "Unknown");
            ClassicAssert.AreEqual(3, vector.Length);
            ClassicAssert.AreEqual(new double[] { 0, 1, 1 }, vector.FullValues);

            vector = instance.GetFullVector("one", "one", "one");
            ClassicAssert.AreEqual(new double[] { 0, 0, 1 }, vector.FullValues);
        }

        [Test]
        public void Arguments()
        {
            ClassicAssert.Throws<ArgumentException>(() => instance.AddWord(null));
            ClassicAssert.Throws<ArgumentNullException>(() => instance.GetFullVector(null));
        }

        [Test]
        public void SimpleTestCase()
        {
            instance.AddWord("One");
            instance.AddWord("Two");
            instance.AddWord("Three");
            var vector = instance.GetFullVector("One", "three");
            ClassicAssert.AreEqual(3, vector.Length);
            ClassicAssert.AreEqual(2, vector.Cells.Length);
        }

    }
}
