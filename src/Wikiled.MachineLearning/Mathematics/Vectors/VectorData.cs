using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class VectorData : IEnumerable<VectorCell>
    {
        private VectorCell[] cells;

        private Dictionary<int, VectorCell> dataTableValues;

        private Lazy<INormalize> normalization;

        private Lazy<VectorCell[]> normalizedData;

        /// <summary>
        ///     Required for XML serialization
        /// </summary>
        public VectorData()
        {
        }

        public VectorData(VectorCell[] data, int length, double rho, NormalizationType normalizationType)
            : this(data, length, normalizationType)
        {
            RHO = rho;
        }

        public VectorData(VectorCell[] data, int length, NormalizationType normalizationType = NormalizationType.None)
        {
            Init(data, length, normalizationType);
        }

        public VectorCell[] Cells => cells ?? (cells = DataTable.Values.ToArray());

        public double[] SparseValues => Cells.Select(item => item.Calculated).ToArray();

        public double[] FullValues
        {
            get
            {
                double[] result = new double[Length];
                for (int i = 0; i < Length; i++)
                {
                    DataTable.TryGetValue(i, out var value);
                    result[i] = value?.Calculated ?? 0;
                }

                return result;
            }
        }

        public Dictionary<int, VectorCell> DataTable
        {
            get
            {
                return dataTableValues ?? (dataTableValues = normalizedData.Value.ToDictionary(item => item.Index, item => item));
            }
        }

        public string Name { get; set; }

        public int Length { get; private set; }

        public INormalize Normalization => normalization.Value;

        public VectorCell[] OriginalCells { get; private set; }

        public double RHO { get; }
     
        public VectorCell this[int index]
        {
            get
            {
                if (index > Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return DataTable.TryGetValue(index, out var outCell) ? outCell : new VectorCell(index, new SimpleCell(index.ToString(), 0), 0);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Vector:");
            foreach (var cell in this)
            {
                builder.AppendFormat(" {0}", cell);
            }

            return builder.ToString();
        }

        public int[] BuildOccurenceVector()
        {
            int[] vector = new int[Length];
            foreach (var cell in DataTable)
            {
                vector[cell.Value.Index] = (int)cell.Value.Data.Value;
            }

            return vector;
        }

        public void ChangeNormalization(NormalizationType normalizationType)
        {
            Init(OriginalCells, Length, normalizationType);
        }

        public IEnumerator<VectorCell> GetEnumerator()
        {
            return new VectorDataEnumerator(this);
        }

        public void NormalizeByCoef(double coef)
        {
            foreach (VectorCell vectorCell in Cells)
            {
                vectorCell.X = vectorCell.X / coef;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Init(VectorCell[] data, int length, NormalizationType normalizationType)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            dataTableValues = null;
            cells = null;
            data = data.OrderBy(item => item.Index).ToArray();
            OriginalCells = data;
            Length = length;
            if (data.Length > Length ||
                (data.Length > 0 && data.Max(item => item.Index) >= Length))
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            normalization = new Lazy<INormalize>(() => data.Select(item => item.X).Normalize(normalizationType));
            normalizedData = new Lazy<VectorCell[]>(
                () =>
                    {
                        var normalized = Normalization.GetNormalized.ToArray();
                        VectorCell[] preparedData = new VectorCell[data.Length];

                        for (int i = 0; i < data.Length; i++)
                        {
                            var existing = data[i];
                            var current = new VectorCell(existing.Index, (ICell)existing.Data.Clone(), existing.Theta);
                            current.X = normalized[i];
                            preparedData[i] = current;
                        }

                        return preparedData;
                    });
        }
    }
}
