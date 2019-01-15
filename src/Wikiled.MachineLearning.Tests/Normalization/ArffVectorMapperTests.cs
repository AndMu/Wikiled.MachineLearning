using System.Linq;
using NUnit.Framework;
using Wikiled.Arff.Logic;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Tests.Normalization
{
    [TestFixture]
    public class ArffVectorMapperTests
    {
        [Test]
        public void GetVectors()
        {
            var dataset = ArffDataSet.CreateSimple("Test");
            var document = dataset.GetOrCreateDocument("one");
            document.AddRecord("T_One").Value = 1;
            document.AddRecord("T_Two").Value = 2;
            document.AddRecord("Three").Value = 2;

            document = dataset.GetOrCreateDocument("Two");
            document.AddRecord("T_One").Value = 1;
            document.AddRecord("T_Two").Value = 2;
            document.AddRecord("Three").Value = 2;

            document = dataset.GetOrCreateDocument("3");
            document.AddRecord("T_One").Value = 2;
            document.AddRecord("T_Two").Value = 1;
            document.AddRecord("Three").Value = 1;

            document = dataset.GetOrCreateDocument("4");
            document.AddRecord("T_One").Value = 2;
            document.AddRecord("T_Two").Value = 1;
            document.AddRecord("Three").Value = 1;

            var mapper = new ArffVectorMapper(dataset);
            var vectors = mapper.GetVectors().ToArray();
            Assert.AreEqual(4, vectors.Length);
            Assert.AreEqual(2, vectors[0].Cells[0].Calculated);
            Assert.AreEqual(1, vectors[0].Cells[1].Calculated);
            Assert.AreEqual(1, vectors[0].Cells[2].Calculated);
        }
    }
}
