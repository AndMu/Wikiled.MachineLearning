using System;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public interface ITracker : IDisposable
    {
        string Name { get; }

        string Type { get; }

        IObservable<RatingRecord> Ratings { get; }

        void TrimOlder(TimeSpan maxTrack);

        void AddRating(RatingRecord rating);

        bool IsTracked(string id);

        double? CalculateAverageRating(int lastHours = 24);

        int Count(bool withRating = true, int lastHours = 24);
    }
}