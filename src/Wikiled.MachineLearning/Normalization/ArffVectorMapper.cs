using System;
using System.Collections.Generic;
using System.Linq;
using Wikiled.Arff.Extensions;
using Wikiled.Arff.Logic;
using Wikiled.Arff.Logic.Headers;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Normalization
{
    public class ArffVectorMapper
    {
        private readonly Dictionary<IHeader, int> featureTable;

        public ArffVectorMapper(IArffDataSet dataSet)
        {
            if (dataSet is null)
            {
                throw new ArgumentNullException(nameof(dataSet));
            }

            dataSet.Header.CreateHeader = false;
            DataSet = dataSet;
            featureTable = dataSet.GetFeatureTable();
        }

        public IArffDataSet DataSet { get; }

        public IEnumerable<VectorData> GetVectors()
        {
            foreach (var row in DataSet.Documents.OrderBy(item => item.Id))
            {
                var vectorCells = new List<VectorCell>();
                foreach (var record in row.GetRecords())
                {
                    vectorCells.Add(GetCell(record));
                }

                yield return new VectorData(vectorCells.ToArray(), featureTable.Count)
                             {
                                 Name = row.Id
                             };
            }
        }

        private VectorCell GetCell(DataRecord record)
        {
            if (!featureTable.TryGetValue(record.Header, out var index))
            {
                return null;
            }

            var cellItem = new VectorCell(index, new SimpleCell(record.Header.Name, Convert.ToDouble(record.Value)), 1);
            return cellItem;
        }
    }
}
