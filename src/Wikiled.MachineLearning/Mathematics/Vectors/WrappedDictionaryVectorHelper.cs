using System;
using System.Collections.Generic;
using System.Linq;
using Wikiled.Core.Utility.Arguments;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class WrappedDictionaryVectorHelper : IDictionaryVectorHelper
    {
        private readonly Dictionary<string, VectorCell> table;

        private readonly Dictionary<VectorCell, double> tableOfAddedItems = new Dictionary<VectorCell, double>();

        private readonly VectorData origin;

        public WrappedDictionaryVectorHelper(VectorData origin)
        {
            Guard.NotNull(() => origin, origin);
            this.origin = origin;
            table = origin.OriginalCells.ToDictionary(item => item.Data.Name, item => item, StringComparer.OrdinalIgnoreCase);
        }

        public void Add(string name, double value)
        {
            VectorCell cell;
            if (!table.TryGetValue(name, out cell))
            {
                return;
            }

            double total = 0;
            tableOfAddedItems.TryGetValue(cell, out total);
            total += value;
            tableOfAddedItems[cell] = total;
            Total += value;
        }

        public VectorData GetFullVector()
        {
            VectorCell[] cells = tableOfAddedItems
                .Select(item => new VectorCell(item.Key.Index,
                    new SimpleCell(item.Key.Data.Name, item.Value),
                    item.Key.Theta))
                .ToArray();
            return new VectorData(cells, origin.Length, origin.RHO, origin.Normalization.Type);
        }

        public double Total { get; private set; }
    }
}
