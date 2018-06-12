using System;
using System.Collections.Generic;
using System.Linq;
using Wikiled.Common.Arguments;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class DictionaryVectorHelper
    {
        private readonly Dictionary<string, int> table = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public void AddToDictionary(string name)
        {
            Guard.NotNullOrEmpty(() => name, name);
            if (!table.TryGetValue(name, out var existingValue))
            {
                table[name] = table.Count;
            }
        }

        public VectorData GetFullVector(params string[] words)
        {
            Guard.NotNull(() => words, words);
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
