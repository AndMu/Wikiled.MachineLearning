using System;
using System.Collections.Generic;
using System.Linq;
using Wikiled.Arff.Normalization;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class DictionaryVectorHelper : IDictionaryVectorHelper
    {
        readonly Dictionary<string, double> table = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        public void Add(string name, double value)
        {
            double existingValue;
            table.TryGetValue(name, out existingValue);
            existingValue += value;
            table[name] = existingValue;
            Total += value;
        }

        public VectorData GetFullVector()
        {
            List<VectorCell> cells = new List<VectorCell>();
            int index = 0;
            foreach (var record in table.OrderByDescending(item => item.Value))
            {
                cells.Add(new VectorCell(index, new SimpleCell(record.Key, record.Value), 1));
                index++;
            }

            return new VectorData(cells.ToArray(), table.Count, NormalizationType.None);
        }

        public double Total { get; private set; }
    }
}
