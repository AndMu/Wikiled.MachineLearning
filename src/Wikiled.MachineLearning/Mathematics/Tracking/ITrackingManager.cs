namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public interface ITrackingManager
    {
        ITracker Resolve(string key, string type);
    }
}