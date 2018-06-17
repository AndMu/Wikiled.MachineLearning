using System;
using System.IO;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors
{
    [TestFixture]
    public class JsonVectorSerializationTests
    {
        [Test]
        public void TestSerialization()
        {
            var file = Path.Combine(TestContext.CurrentContext.TestDirectory, "data.json");
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            var serialization = new JsonVectorSerialization(file);
            DictionaryVectorHelper helper = new DictionaryVectorHelper();
            helper.AddToDictionary("One");
            helper.AddToDictionary("Two");
            helper.AddToDictionary("Three");
            var vector1 = helper.GetFullVector("One", "Two");
            var vector2 = helper.GetFullVector("One", "Three");
            serialization.Serialize(new[] { vector1, vector2 });
        }

        [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonVectorSerialization(null));
        }
    }
}