namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public interface ITrackerFactory
    {
        ITracker Construct(string name);
    }
}