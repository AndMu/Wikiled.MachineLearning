using System.Linq;

namespace Wikiled.MachineLearning.Mathematics.Indicators
{
    public class SimpleMovingAverage : BaseIndicator<double, double>
    {
        public SimpleMovingAverage(int length)
        {
            base.Length = length;
        }
            
        protected override double CalculateValue(double value)
        {
            var lastValue = LastValue;
            lastValue += value / Length;
            if (IsFormed)
            {
                lastValue -= Results.First() / Length;
                Results.Dequeue();
            }

            Results.Enqueue(value);
            return lastValue;
        }
    }
}
