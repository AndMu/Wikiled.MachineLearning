using System;
using System.Collections.Generic;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class OneHotEncoder : IWordVectorEncoder
    {
        private readonly Dictionary<string, int> table = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public double Total => table.Count;

        public void AddWords(params string[] words)
        {
            foreach (var word in words)
            {
                AddWord(word);
            }
        }

        public void AddWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(word));
            }

            if (!table.TryGetValue(word, out _))
            {
                table[word] = table.Count;
            }
        }

        public VectorData GetFullVector(params string[] words)
        {
            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            var cells = new List<VectorCell>();
            foreach (var word in words)
            {
                if (table.ContainsKey(word))
                {
                    cells.Add(new VectorCell(table[word], new SimpleCell(word, 1), 1));
                }
            }

            return new VectorData(cells.ToArray(), table.Count, NormalizationType.None);
        }
    }
}
