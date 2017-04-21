using System;

namespace Wikiled.MachineLearning.Mathematics
{
    public static class RatingExtension
    {
        public static double GetPercentage(this double value)
        {
            if (value > 1 ||
                value < -1)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            value += 1;
            value /= 2;
            value = value > 1 ? 1 : value;
            return Math.Round(value * 100, 2);
        }

        public static double GetStars(this double value)
        {
            var offset = Math.Round(value % 0.5, 2);
            offset = offset > 0.25 ? 0.5 - offset : -offset;
            return Math.Round(value + offset, 1);
        }

        public static double GetStarsFromProbability(this double value)
        {
            return value * 4 + 1;
        }

        public static double GetProbability(this double value, double step)
        {
            if (value > step)
            {
                return 1;
            }

            if (value < -step)
            {
                return 0;
            }

            return (step + value) / (2 * step);
        }
    }
}
