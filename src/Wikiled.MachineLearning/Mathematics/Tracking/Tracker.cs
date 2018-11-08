using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Wikiled.Common.Utilities.Config;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public class Tracker : ITracker
    {
        private readonly IApplicationConfiguration config;

        private readonly TimeSpan maxTrack;

        private readonly ConcurrentQueue<RatingRecord> ratings = new ConcurrentQueue<RatingRecord>();

        public Tracker(IApplicationConfiguration config, TimeSpan maxTrack)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.maxTrack = maxTrack;
        }

        public void AddRating(RatingRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            var yesterday = config.Now.Subtract(maxTrack);
            ratings.Enqueue(record);
            while (ratings.TryPeek(out var item) &&
                   item.Date < yesterday &&
                   ratings.TryDequeue(out item))
            {
            }
        }

        public double? AverageSentiment(int lastHours = 24)
        {
            var sentiment = GetSentiments(lastHours).ToArray();
            if (sentiment.Length == 0)
            {
                return null;
            }

            return sentiment.Average();
        }

        public int TotalWithSentiment(int lastHours = 24)
        {
            return GetSentiments(lastHours).Count();
        }

        private IEnumerable<double> GetSentiments(int lastHours = 24)
        {
            var time = config.Now;
            time = time.AddHours(-lastHours);
            return ratings.Where(item => item.Rating.HasValue && item.Date > time).Select(item => item.Rating.Value);
        }
    }
}
