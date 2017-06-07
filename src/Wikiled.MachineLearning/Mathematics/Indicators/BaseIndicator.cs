using System;
using System.Collections.Generic;

namespace Wikiled.MachineLearning.Mathematics.Indicators
{
    public abstract class BaseIndicator<T, TResult> : IIndicator
    {
        private int length;

        protected BaseIndicator()
        {
            Results = new Queue<T>();
            LastValue = default(TResult);
            Value = default(TResult);
        }

        /// <summary>
        /// Сформирован ли индикатор.
        /// </summary>
        public virtual bool IsFormed
        {
            get { return Results.Count >= Length; }
        }

        /// <summary>
        /// Длина периода. По-умолчанию длина равна 1.
        /// </summary>
        public virtual int Length
        {
            get { return length; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("value", value, "Длина периода задана неправильно.");
                }

                length = value;
                Reset();
            }
        }


        public int Count
        {
            get { return Results.Count; }
        }

        public TResult LastValue { get; protected set; }

        public TResult Value { get; protected set; }

        protected Queue<T> Results { get; private set; }

        public virtual TResult Add(T value)
        {
            LastValue = Value;
            Value = CalculateValue(value);
            return Value;
        }

        /// <summary>
        /// Сбросить состояние индикатора на первоначальное. Метод вызывается каждый раз, когда меняются первоначальные настройки (например, длина периода).
        /// </summary>
        public virtual void Reset()
        {
            Results.Clear();
        }

        public void Add(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                Add(value);
            }
        }

        protected abstract TResult CalculateValue(T value);
    }
}