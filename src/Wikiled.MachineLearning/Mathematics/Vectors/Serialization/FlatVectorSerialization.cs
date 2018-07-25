using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wikiled.Common.Extensions;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Mathematics.Vectors.Serialization
{
    public class FlatVectorSerialization : IVectorSerialization
    {
        private readonly string path;

        public FlatVectorSerialization(string path)
        {
            this.path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public void Serialize(IEnumerable<VectorData> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            using (var streamWriter = new StreamWriter(path))
            {
                foreach (var vector in data)
                {
                    streamWriter.WriteLine(vector.FullValues.Select(item => item.ToString()).AccumulateItems(","));
                }
            }
        }

        public IEnumerable<VectorData> Deserialize()
        {
            using (var streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var data = line.Split(',').Select(double.Parse).ToArray();
                    List<VectorCell> cells = new List<VectorCell>(data.Length);
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] != 0)
                        {
                            cells.Add(new VectorCell(i, new SimpleCell(i.ToString(), data[i]), 1));
                        }
                    }

                    yield return new VectorData(cells.ToArray(), data.Length, NormalizationType.None);
                }
            }
        }
    }
}
