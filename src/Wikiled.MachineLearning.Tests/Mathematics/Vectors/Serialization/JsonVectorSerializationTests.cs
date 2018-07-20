using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Vectors;
using Wikiled.MachineLearning.Mathematics.Vectors.Serialization;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors.Serialization
{
    [TestFixture]
    public class JsonVectorSerializationTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void TestSerialization(bool useName)
        {
            var file = Path.Combine(TestContext.CurrentContext.TestDirectory, "data.json");
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            var serialization = new JsonVectorSerialization(file, useName);
            DictionaryVectorHelper helper = new DictionaryVectorHelper();
            helper.AddToDictionary("One");
            helper.AddToDictionary("Two");
            helper.AddToDictionary("Three");
            var vector1 = helper.GetFullVector("One", "Two");
            var vector2 = helper.GetFullVector("One", "Three");
            serialization.Serialize(new[] { vector1, vector2 });
            var result = serialization.Deserialize().ToArray();
            Assert.AreEqual(2, result.Length);

            Assert.AreEqual(1, result[0].Cells[0].X);
            Assert.AreEqual(0, result[0].Cells[0].Index);
            Assert.AreEqual(1, result[0].Cells[1].X);
            Assert.AreEqual(1, result[0].Cells[1].Index);

            Assert.AreEqual(1, result[1].Cells[0].X);
            Assert.AreEqual(0, result[1].Cells[0].Index);
            Assert.AreEqual(1, result[1].Cells[1].X);
            Assert.AreEqual(2, result[1].Cells[1].Index);
        }

        [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonVectorSerialization(null));
        }
    }
}