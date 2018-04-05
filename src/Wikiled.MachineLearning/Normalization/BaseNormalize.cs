using System;
using System.Collections.Generic;
using System.Linq;

namespace Wikiled.MachineLearning.Normalization
{
    public abstract class BaseNormalize : INormalize
    {
        private readonly Lazy<double> coeficient;

        private readonly Lazy<IEnumerable<double>> normalized;

        protected BaseNormalize(IEnumerable<double> source)
        {
            Source = source;
            coeficient = new Lazy<double>(CalculateCoef);
            normalized = new Lazy<IEnumerable<double>>(() => source.Select(item => Math.Round(item / Coeficient, 10)));
        }

        public abstract NormalizationType Type { get; }

        public double Coeficient
        {
            get
            {
                var coef = coeficient.Value;
                return Math.Abs(coef - 0) < 0.0000001 ? 1 : coef;
            }
        }

        public IEnumerable<double> GetNormalized => normalized.Value;

        protected IEnumerable<double> Source { get; }

        protected abstract double CalculateCoef();
    }
}
