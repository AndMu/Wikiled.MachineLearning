using System;
using Microsoft.Extensions.Logging;
using Wikiled.Common.Logging;

namespace Wikiled.MachineLearning.Mathematics
{
    public static class RatingCalculator
    {
        private static readonly ILogger log = ApplicationLogging.CreateLogger("RatingCalculator");

        public static int? CalculateStar(double? value)
        {
            var stars = CalculateStarsRating(value);
            if (!stars.HasValue)
            {
                return null;
            }

            return (int)Math.Round(stars.Value);
        }

        public static double? CalculateStarsRating(double? value)
        {
            return 2 * value + 3;
        }

        public static double? ConvertToRaw(double star)
        {
            return (star - 3) / 2;
        }

        public static double Calculate(double positive, double negative)
        {
            if (positive < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(positive));
            }

            if (negative < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(negative));
            }

            int coefficient = 2;
            if (positive == 0 &&
                negative == 0)
            {
                return 0;
            }

            double min = 0.0001;
            positive += min;
            negative += min;
            double rating = Math.Log(positive / negative, 2);

            if (positive == min ||
                rating < -coefficient)
            {
                rating = -coefficient;
            }
            else if (negative == min || rating > coefficient)
            {
                rating = coefficient;
            }

            rating = rating / coefficient;
            log.LogDebug("Positive: {0} Negative: {1} Rating: {2}", positive, negative, rating);
            return rating;
        }
    }
}
