using System;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public interface ITracker
    {
        void TrimOlder(TimeSpan maxTrack);

        void AddRating(RatingRecord rating);

        double? AverageSentiment(int lastHours = 24);

        int Count(bool withSentiment = true, int lastHours = 24);
    }
}