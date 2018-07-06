using System;
using System.Collections.Generic;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class DictionaryVectorHelper
    {
        private readonly Dictionary<string, int> table = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public void AddToDictionary(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (!table.TryGetValue(name, out _))
            {
                table[name] = table.Count;
            }
        }

        public VectorData GetFullVector(params string[] words)
        {
            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            List<VectorCell> cells = new List<VectorCell>();
            foreach (var word in words)
            {
                cells.Add(new VectorCell(table[word], new SimpleCell(word, 1), 1));
            }

            return new VectorData(cells.ToArray(), table.Count, NormalizationType.None);
        }

        public double Total => table.Count;
    }
}
