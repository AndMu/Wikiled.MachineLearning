using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Wikiled.Common.Utilities.Config;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public class Tracker : ITracker
    {
        private readonly IApplicationConfiguration config;

        private readonly List<RatingRecord> ratings = new List<RatingRecord>();

        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public Tracker(IApplicationConfiguration config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void TrimOlder(TimeSpan maxTrack)
        {
            DateTime cutOff = config.Now.Subtract(maxTrack);
            _lock.EnterWriteLock();
            try
            {
                foreach (RatingRecord item in ratings.Where(item => item.Date < cutOff).ToArray())
                {
                    ratings.Remove(item);
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void AddRating(RatingRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            _lock.EnterWriteLock();
            try
            {
                ratings.Add(record);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public double? AverageSentiment(int lastHours = 24)
        {
            _lock.EnterReadLock();
            try
            {
                double[] sentiment = GetValues(true, lastHours).ToArray();
                if (sentiment.Length == 0)
                {
                    return null;
                }

                return sentiment.Average();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int Count(bool withSentiment = true, int lastHours = 24)
        {
            _lock.EnterReadLock();
            try
            {
                return GetValues(withSentiment, lastHours).Count();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        private IEnumerable<double> GetValues(bool withSentiment, int lastHours = 24)
        {
            DateTime time = config.Now;
            time = time.AddHours(-lastHours);
            return ratings.Where(item => (!withSentiment || item.Rating.HasValue) && item.Date > time).Select(item => item.Rating.Value);
        }
    }
}
