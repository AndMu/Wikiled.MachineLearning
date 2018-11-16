using System;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public interface IRatingStream
    {
        IObservable<(ITracker, RatingRecord)> Stream { get; }
    }
}
