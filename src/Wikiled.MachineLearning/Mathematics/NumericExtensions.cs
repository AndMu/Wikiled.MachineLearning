using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Wikiled.Common.Extensions;

namespace Wikiled.MachineLearning.Mathematics
{
    /// <summary>
    ///     Convert to Radians.
    /// </summary>
    /// <returns>The value in radians</returns>
    public static class NumericExtensions
    {
        public static Vector2 GetMonthVector(this DateTime date)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var degrees = GetDegrees(date.Day, daysInMonth);
            return GetVectorByDegrees(degrees);
        }

        public static Vector2 GetVector(this double degrees)
        {
            return GetVectorByDegrees(degrees);
        }

        public static Vector2 GetWeekVector(this DateTime date)
        {
            var degrees = GetDegrees((int)date.DayOfWeek, 7);
            return GetVectorByDegrees(degrees);
        }

        public static Vector2 GetYearVector(this DateTime date)
        {
            var daysInTheYear = new DateTime(date.Year, 12, 31).DayOfYear;
            var degrees = GetDegrees(date.DayOfYear, daysInTheYear);
            return GetVectorByDegrees(degrees);
        }

        public static IEnumerable<Array> Shuffle(this Random random, params Array[] arrays)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (arrays == null)
            {
                throw new ArgumentNullException(nameof(arrays));
            }

            if (arrays.Length < 1 ||
                arrays.Any(item => item.Length != arrays[0].Length))
            {
                throw new ArgumentOutOfRangeException(nameof(arrays));
            }

            var indexes = Enumerable.Range(0, arrays[0].Length).ToList();
            var shuffled = indexes.Shuffle(random).ToArray();
            foreach (var array in arrays)
            {
                yield return shuffled.Select(index => array.GetValue(index)).ToArray();
            }
        }

        public static double ToRadians(this double degrees)
        {
            return Math.PI * degrees / 180;
        }

        private static double GetDegrees(int value, int total)
        {
            return 360 * value / (double)total;
        }

        private static Vector2 GetVectorByDegrees(double degrees)
        {
            var radians = degrees.ToRadians();
            return new Vector2((float)Math.Sin(radians), (float)Math.Cos(radians));
        }
    }
}
