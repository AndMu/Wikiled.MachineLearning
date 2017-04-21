using System.Linq;

namespace Wikiled.MachineLearning.Mathematics.Indicators
{
    public class ExponentialMovingAverage : BaseIndicator<double, double>
    {
        private double multiplier;

        public ExponentialMovingAverage(int length)
        {
            base.Length = length;
        }

        /// <summary>
        /// Сбросить состояние индикатора на первоначальное. Метод вызывается каждый раз, когда меняются первоначальные настройки (например, длина периода).
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            multiplier = 2f / (Length + 1);
        }

        protected override double CalculateValue(double value)
        {
            Results.Enqueue(value);
            if (!IsFormed)
            {
                return LastValue;
            }

            if (Count == Length)
            {
                // Начальное значение - простая скользящая средняя.
                return Results.Sum() / Length;
            }

            Results.Dequeue();
            return (value - LastValue) * multiplier + LastValue;
        }
    }
}
