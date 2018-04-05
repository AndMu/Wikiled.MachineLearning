using System;
using System.Linq;
using Wikiled.Arff.Persistence;
using Wikiled.Arff.Persistence.Headers;

namespace Wikiled.MachineLearning.Normalization
{
    public static class ArffExtensions
    {
        public void Normalize(this IArffDataSet dataSet, NormalizationType type)
        {
            if (dataSet.Normalization != NormalizationType.None)
            {
                throw new ArgumentOutOfRangeException("type", "Data is already normalized");
            }

            if (type == NormalizationType.None)
            {
                return;
            }

            dataSet.Normalization = type;
            foreach (var doc in dataSet.Documents)
            {
                var words = doc.GetRecords()
                               .Where(item => item.Header is NumericHeader)
                               .ToArray();

                var normalized = words
                                 .Select(item => Convert.ToDouble(item.Value))
                                 .Normalize(type)
                                 .GetNormalized
                                 .ToArray();

                for (var i = 0; i < words.Length; i++)
                {
                    words[i].Value = normalized[i];
                }
            }
        }
    }
}
