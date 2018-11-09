using System;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public interface IExpireTracking : IDisposable
    {
        void Register(ITracker tracker);
    }
}