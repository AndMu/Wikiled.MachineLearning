using System;
using System.Numerics;

namespace Wikiled.MachineLearning.Mathematics
{
    /// <summary>
    /// Convert to Radians.
    /// </summary>
    /// <returns>The value in radians</returns>
    public static class NumericExtensions
    {
        public static double ToRadians(this double degrees)
        {
            return Math.PI * degrees / 180;
        }

        public static Vector2 GetVector(this double degrees)
        {
            return GetVectorByDegrees(degrees);
        }

        public static Vector2 GetYearVector(this DateTime date)
        {
            var daysInTheYear = new DateTime(date.Year, 12, 31).DayOfYear;
            var degrees = GetDegrees(date.DayOfYear, daysInTheYear);
            return GetVectorByDegrees(degrees);
        }

        public static Vector2 GetWeekVector(this DateTime date)
        {
            var degrees = GetDegrees(((int)date.DayOfWeek), 7);
            return GetVectorByDegrees(degrees);
        }

        public static Vector2 GetMonthVector(this DateTime date)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var degrees = GetDegrees(date.Day, daysInMonth);
            return GetVectorByDegrees(degrees);
        }

        private static Vector2 GetVectorByDegrees(double degrees)
        {
            var radians = degrees.ToRadians();
            return new Vector2((float)Math.Sin(radians), (float)Math.Cos(radians));
        }

        private static double GetDegrees(int value, int total)
        {
            return 360 * value / (double)total;
        }

    }
}
