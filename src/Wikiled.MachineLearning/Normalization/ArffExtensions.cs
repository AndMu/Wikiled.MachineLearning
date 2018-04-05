using System.Collections.Generic;
using System.Linq;
using Wikiled.Arff.Extensions;
using Wikiled.Arff.Persistence;

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
    }
}
