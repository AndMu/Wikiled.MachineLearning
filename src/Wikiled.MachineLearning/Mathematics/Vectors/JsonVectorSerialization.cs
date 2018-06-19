using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class JsonVectorSerialization : IJsonVectorSerialization
    {
        private readonly string path;

        public JsonVectorSerialization(string path)
        {
            this.path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public void Serialize(IEnumerable<VectorData> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            using (var streamReader = new StreamWriter(path))
            using (JsonTextWriter writer = new JsonTextWriter(streamReader))
            {
                writer.Formatting = Formatting.Indented;
                int recordId = 0;
                writer.WriteStartObject();
                foreach (var record in data)
                {
                    writer.WritePropertyName(record.Name ?? recordId.ToString());
                    writer.WriteStartObject();
                    writer.WritePropertyName("Vector");
                    writer.WriteStartObject();
                    foreach (var cell in record.DataTable)
                    {
                        writer.WritePropertyName(cell.Key.ToString());
                        writer.WriteValue(cell.Value.Calculated);
                    }

                    writer.WriteEndObject();
                    writer.WriteEndObject();
                    
                    recordId++;
                }

                writer.WriteEndObject();
            }
        }
    }
}
