using System;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public class RatingRecord
    {
        public RatingRecord(DateTime date, double? rating)
        {
            Date = date;
            Rating = rating;
        }

        public DateTime Date { get; }

        public double? Rating { get; }
    }
}
