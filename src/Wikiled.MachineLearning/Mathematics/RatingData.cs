using System;
using System.Collections.Generic;

namespace Wikiled.MachineLearning.Mathematics
{
    public class RatingData : ICloneable
    {
        public bool HasValue => Positive > 0 || Negative > 0;

        public bool IsPositive => RawRating > 0;

        public bool IsStrong
        {
            get
            {
                var difference = Positive - Negative;
                if (Math.Abs(difference) < 4)
                {
                    return false;
                }

                if (Positive == 0 ||
                    Negative == 0)
                {
                    return true;
                }

                var ratio = Positive > Negative ? Positive / Negative : Negative / Positive;
                return Math.Abs(ratio) >= 2;
            }
        }

        public double Negative { get; private set; }

        public double Positive { get; private set; }

        public double? RawRating
        {
            get => HasValue ? RatingCalculator.Calculate(Positive, Negative) : (double?)null;
            set
            {
            }
        }

        public double? StarsRating
        {
            get => RatingCalculator.CalculateStarsRating(RawRating);
            set
            {
            }
        }

        public void AddPositive(double sentiment)
        {
            if (sentiment < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sentiment));
            }

            Positive += sentiment;
        }

        public void AddNegative(double sentiment)
        {
            if (sentiment < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sentiment));
            }

            Negative += sentiment;
        }

        public void AddSetiment(double sentiment)
        {
            if (sentiment > 0)
            {
                AddPositive(sentiment);
            }
            else
            {
                AddNegative(Math.Abs(sentiment));
            }
        }

        public static RatingData Accumulate(IEnumerable<RatingData> items)
        {
            var data = new RatingData();
            foreach (var ratingData in items)
            {
                data.Positive += ratingData.Positive;
                data.Negative += ratingData.Negative;
            }

            return data;
        }

        public object Clone()
        {
            return new RatingData
            {
                Negative = Negative,
                Positive = Positive
            };
        }
    }
}
