using System;
using System.Collections.Generic;
using System.Linq;
using Wikiled.Arff.Extensions;
using Wikiled.Arff.Persistence;
using Wikiled.Arff.Persistence.Headers;

namespace Wikiled.MachineLearning.Normalization
{
    public static class ArffExtensions
    {
        public static IEnumerable<(int? Y, double[] X)> GetDataNormalized(this IArffDataSet dataSet, NormalizationType type)
        {
            var data = dataSet.GetData();
            foreach (var item in data)
            {
                if (type == NormalizationType.None)
                {
                    yield return item;
                }
                
                var normalized = item.X
                                 .Normalize(type)
                                 .GetNormalized
                                 .ToArray();

                yield return (item.Y, normalized);
            }
        }

        public static double[][] GetRawData(this IArffDataSet dataset, Func<DataRecord, double> getValue = null, Func<IHeader, bool> include = null)
        {
            if (dataset == null)
            {
                throw new ArgumentNullException(nameof(dataset));
            }

            if (getValue == null)
            {
                getValue = item =>
                {
                    if (item.Value is int)
                    {
                        return (double)(int)item.Value;
                    }

                    return (double)item.Value;
                };
            }

            double[][] data = new double[dataset.TotalDocuments][];
            var headers = dataset.Header.Where(item => include == null || include(item)).OrderBy(item => dataset.Header.GetIndex(item)).ToArray();
            var documents = dataset.Documents.ToArray();
            for (int i = 0; i < documents.Length; i++)
            {
                data[i] = new double[headers.Length];
                for (int i2 = 0; i2 < headers.Length; i2++)
                {
                    double valueItem = 0;
                    if (documents[i].HeadersTable.TryGetValue(headers[i2], out var value))
                    {
                        valueItem = getValue(value);
                    }

                    data[i][i2] = valueItem;
                }
            }

            return data;
        }
    }
}
