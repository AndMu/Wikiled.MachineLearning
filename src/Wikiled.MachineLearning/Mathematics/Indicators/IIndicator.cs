namespace Wikiled.MachineLearning.Mathematics.Indicators
{
    public interface IIndicator
    {
        int Count { get; }

        /// <summary>
        /// Длина периода. По-умолчанию длина равна 1.
        /// </summary>
        int Length { get; set; }

        /// <summary>
        /// Сформирован ли индикатор.
        /// </summary>
        bool IsFormed { get; }

        /// <summary>
        /// Сбросить состояние индикатора на первоначальное. Метод вызывается каждый раз, когда меняются первоначальные настройки (например, длина периода).
        /// </summary>
        void Reset();
    }
}