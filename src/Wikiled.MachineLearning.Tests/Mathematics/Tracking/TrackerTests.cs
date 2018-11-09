using Moq;
using NUnit.Framework;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Wikiled.Common.Utilities.Config;
using Wikiled.MachineLearning.Mathematics.Tracking;

namespace Wikiled.MachineLearning.Tests.Mathematics.Tracking
{
    [TestFixture]
    public class TrackerTests
    {
        private ILogger<Tracker> logger;

        private Mock<IApplicationConfiguration> mockApplicationConfiguration;

        private Tracker instance;

        [SetUp]
        public void SetUp()
        {
            logger = new NullLogger<Tracker>();
            mockApplicationConfiguration = new Mock<IApplicationConfiguration>();
            instance = CreateTracker();
            mockApplicationConfiguration.Setup(item => item.Now).Returns(new DateTime(2016, 01, 10));
        }

        [Test]
        public void AddRatingArguments()
        {
            Assert.Throws<ArgumentNullException>(() => instance.AddRating(null));
        }

        [Test]
        public void TrimOlder()
        {
            var result = instance.AverageSentiment();
            Assert.IsNull(result);
            instance.AddRating(new RatingRecord("1", new DateTime(2016, 01, 10), 10));
            instance.AddRating(new RatingRecord("2", new DateTime(2016, 01, 10), 10));

            mockApplicationConfiguration.Setup(item => item.Now).Returns(new DateTime(2016, 01, 11));
            Assert.AreEqual(0, instance.Count());
            Assert.AreEqual(2, instance.Count(lastHours: 48));
            instance.TrimOlder(TimeSpan.FromHours(1));
            Assert.AreEqual(0, instance.Count(lastHours: 48));
        }

        [Test]
        public void AddSameId()
        {
            instance.AddRating(new RatingRecord("1", new DateTime(2016, 01, 10), 10));
            instance.AddRating(new RatingRecord("1", new DateTime(2016, 01, 10), 10));
            var result = instance.AverageSentiment();
            Assert.AreEqual(1, instance.Count());
            Assert.AreEqual(10, result);
        }

        [Test]
        public void AverageSentiment()
        {
            var result = instance.AverageSentiment();
            Assert.IsNull(result);
            instance.AddRating(new RatingRecord("1", new DateTime(2016, 01, 10), 10));
            instance.AddRating(new RatingRecord("2", new DateTime(2016, 01, 10), 10));
            result = instance.AverageSentiment();
            Assert.AreEqual(2, instance.Count());
            Assert.AreEqual(10, result);

            instance.AddRating(new RatingRecord("3", new DateTime(2016, 01, 11), -20));
            result = instance.AverageSentiment();
            Assert.AreEqual(3, instance.Count());
            Assert.AreEqual(0, result);

            mockApplicationConfiguration.Setup(item => item.Now).Returns(new DateTime(2016, 01, 11));
            result = instance.AverageSentiment();
            Assert.AreEqual(1, instance.Count());
            Assert.AreEqual(-20, result);
        }

        [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new Tracker(logger, null));
            Assert.Throws<ArgumentNullException>(() => new Tracker(null, mockApplicationConfiguration.Object));
            Assert.AreEqual(0, instance.Count());
        }
        
        private Tracker CreateTracker()
        {
            return new Tracker(logger, mockApplicationConfiguration.Object);
        }
    }
}
