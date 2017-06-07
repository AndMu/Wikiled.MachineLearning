using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Statistics;

namespace Wikiled.MachineLearning.Tests.Mathematics.Statistics
{
    [TestFixture]
    public class GrangerCausalityTests
    {
        private double[] chickens;

        private double[] eggs;

        [SetUp]
        public void Setup()
        {
            // based on http://www.econ.uiuc.edu/~econ472/tutorial8.html
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, @"Mathematics\Statistics\data.csv"));
            var data = lines.Select(item => item.Split('\t')).Select(
                item =>
                    new
                    {
                        Year = double.Parse(item[0]),
                        Chickens = double.Parse(item[2]),
                        Eggs = double.Parse(item[1])
                    });

            // can ve verified at http://www.wessa.net/rwasp_grangercausality.wasp
            chickens = data.Select(item => item.Chickens).ToArray();
            eggs = data.Select(item => item.Eggs).ToArray();
        }

        [Test]
        public void TestGranger()
        {
            // kiausinai causina
            var results = GrangerCausality.TestGranger(eggs, chickens, 1);
            Assert.AreEqual(0.8292, Math.Round(results, 4));

            results = GrangerCausality.TestGranger(chickens, eggs, 1);
            Assert.AreEqual(0.2772, Math.Round(results, 4));

            results = GrangerCausality.TestGranger(eggs, chickens, 4);
            Assert.AreEqual(0.8125, Math.Round(results, 4));

            results = GrangerCausality.TestGranger(chickens, eggs, 4);
            Assert.AreEqual(0.0057, Math.Round(results, 4));
        }

        [Test]
        public void MultiTestGranger()
        {
            var results = GrangerCausality.MultiTestGranger(eggs, chickens, 1);
            Assert.AreEqual("Granger Lag: <1> X: <0.829193462613005> Y: <0.277169618232637>", results.ToString());
        }
    }
}
