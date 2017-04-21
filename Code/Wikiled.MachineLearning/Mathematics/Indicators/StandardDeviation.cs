using System;
using System.Linq;

namespace Wikiled.MachineLearning.Mathematics.Indicators
{
    public class StandardDeviation : BaseIndicator<double, double>
    {
        private readonly SimpleMovingAverage sma;

        /// <summary>
        /// Создать <see cref="StandardDeviation"/>.
        /// </summary>
        public StandardDeviation(int length)
        {
            sma = new SimpleMovingAverage(length);
        }

        /// <summary>
        /// Длина периода.
        /// </summary>
        public override int Length
        {
            get
            {
                return sma.Length;
            }
            set
            {
                sma.Length = value;
                Reset();
            }
        }

        /// <summary>
        /// Сформирован ли индикатор.
        /// </summary>
        public override bool IsFormed => sma.IsFormed;

        protected override double CalculateValue(double value)
        {
            var smaValue = sma.Add(value);

            Results.Enqueue(value);

            //считаем значение отклонения в последней точке
            var std = Results.Select(t1 => t1 - smaValue).Select(t => t * t).Sum();

            if (IsFormed)
            {
                Results.Dequeue();
            }

            return Math.Sqrt((double)(std / Length));
        }
    }
}
