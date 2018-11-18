using System.Collections.Generic;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public interface ITrackingManager
    {
        IEnumerable<ITracker> AllTrackers { get; }

        ITracker Resolve(string key, string type);
    }
}