using System;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public class TrackingConfiguration
    {
        public TrackingConfiguration(TimeSpan scanTime, TimeSpan expire)
        {
            ScanTime = scanTime;
            Expire = expire;
        }

        public TimeSpan ScanTime { get; }

        public TimeSpan Expire { get; }
    }
}
