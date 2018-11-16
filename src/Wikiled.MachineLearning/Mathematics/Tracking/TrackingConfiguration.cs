using System;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public class TrackingConfiguration
    {
        public TimeSpan ScanTime { get; set; }

        public TimeSpan Expire { get; set; }

        public string Persistency { get; set; }
    }
}
