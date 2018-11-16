using System;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public interface IRatingStream
    {
        IObservable<(ITracker Tracker, RatingRecord Rating)> Stream { get; }
    }
}
