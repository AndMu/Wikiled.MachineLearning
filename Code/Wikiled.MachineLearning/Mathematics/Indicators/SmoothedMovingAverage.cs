using System.Linq;

namespace Wikiled.MachineLearning.Mathematics.Indicators
{
    /// <summary>
    /// Сглаженное скользящее среднее.
    /// </summary>
    public class SmoothedMovingAverage : BaseIndicator<double, double>
    {
        public SmoothedMovingAverage(int length = 14)
        {
            base.Length = length; 
        }

        protected override double CalculateValue(double value)
        {
            Results.Enqueue(value); 
            if (Count <= Length)
            {
                return Results.Sum() / Count;
            }

            Results.Dequeue();
            return (LastValue * (Length - 1) + value) / Length;
        }
    }
}