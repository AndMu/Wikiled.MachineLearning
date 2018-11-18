using System;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public class RatingRecord
    {
        public RatingRecord(string id, string type, DateTime date, double? rating)
        {
            Date = date;
            Rating = rating;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public string Type { get; }

        public string Id { get; }

        public DateTime Date { get; }

        public double? Rating { get; }
    }
}
