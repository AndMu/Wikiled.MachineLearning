namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public interface ITracker
    {
        void AddRating(RatingRecord rating);

        double? AverageSentiment(int lastHours = 24);

        int TotalWithSentiment(int lastHours = 24);
    }
}