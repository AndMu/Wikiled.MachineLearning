using System;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public interface ITrackingRegister : IDisposable
    {
        void Register(ITracker tracker);
    }
}