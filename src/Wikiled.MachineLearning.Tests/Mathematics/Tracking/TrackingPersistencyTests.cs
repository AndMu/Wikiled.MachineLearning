using System;
using System.IO;
using System.Reactive;
using Microsoft.Reactive.Testing;
using Moq;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Tracking;

namespace Wikiled.MachineLearning.Tests.Mathematics.Tracking
{
    [TestFixture]
    public class TrackingPersistencyTests
    {
        private TrackingConfiguration mockTrackingConfiguration;

        private Mock<IRatingStream> mockRatingStream;

        private TrackingPersistency instance;

        private TestScheduler scheduler;

        [SetUp]
        public void SetUp()
        {
            scheduler = new TestScheduler();
            mockTrackingConfiguration = new TrackingConfiguration();
            mockTrackingConfiguration.Persistency = Path.Combine(TestContext.CurrentContext.TestDirectory, "file.csv");
            if (File.Exists(mockTrackingConfiguration.Persistency))
            {
                File.Delete(mockTrackingConfiguration.Persistency);
            }

            mockRatingStream = new Mock<IRatingStream>();
            var tracker = new Mock<ITracker>();
            var stream = scheduler.CreateHotObservable(
                new Recorded<Notification<(ITracker Tracker, RatingRecord Rating)>>(100, Notification.CreateOnNext((tracker.Object, new RatingRecord("1", DateTime.Now, 2)))));
            mockRatingStream.Setup(item => item.Stream).Returns(stream);
            instance = CreateTrackingPersistency();
        }

        [TearDown]
        public void Teardown()
        {
            instance.Dispose();
        }

       [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new TrackingPersistency(null, mockRatingStream.Object));
            Assert.Throws<ArgumentNullException>(() => new TrackingPersistency(mockTrackingConfiguration, null));
        }

        [Test]
        public void Logic()
        {
            scheduler.AdvanceBy(100);
            instance.Dispose();
            Assert.IsTrue(File.Exists(mockTrackingConfiguration.Persistency));
        }

        private TrackingPersistency CreateTrackingPersistency()
        {
            return new TrackingPersistency(mockTrackingConfiguration, mockRatingStream.Object);
        }
    }
}
