using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Mathematics.Vectors.Serialization
{
    public class JsonVectorSerialization : IVectorSerialization
    {
        private readonly string path;

        private readonly bool useName;

        public JsonVectorSerialization(string path, bool useName = false)
        {
            this.path = path ?? throw new ArgumentNullException(nameof(path));
            this.useName = useName;
        }

        public void Serialize(IEnumerable<VectorData> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            using (var streamWriter = new StreamWriter(path))
            using (JsonTextWriter writer = new JsonTextWriter(streamWriter))
            {
                writer.Formatting = Formatting.Indented;
                int recordId = 0;
                writer.WriteStartObject();
                foreach (var record in data)
                {
                    if (recordId == 0)
                    {
                        writer.WritePropertyName("Normalization");
                        writer.WriteValue(record.Normalization.Type.ToString());
                        writer.WritePropertyName("Length");
                        writer.WriteValue(record.Length);
                    }

                    writer.WritePropertyName(record.Name ?? recordId.ToString());
                    writer.WriteStartObject();
                    writer.WritePropertyName("Vector");
                    writer.WriteStartObject();
                    foreach (var cell in record.DataTable)
                    {
                        if (useName)
                        {
                            writer.WritePropertyName(cell.Value.Data.Name);
                            writer.WriteStartObject();
                            writer.WritePropertyName("Index");
                            writer.WriteValue(cell.Key.ToString());
                            writer.WritePropertyName("Value");
                            writer.WriteValue(cell.Value.Calculated);
                            writer.WriteEndObject();
                        }
                        else
                        {
                            writer.WritePropertyName(cell.Key.ToString());
                            writer.WriteValue(cell.Value.Calculated);
                        }
                    }

                    writer.WriteEndObject();
                    writer.WriteEndObject();
                    recordId++;
                }

                writer.WriteEndObject();
            }
        }

        public IEnumerable<VectorData> Deserialize()
        {
            using (var streamReader = new StreamReader(path))
            using (JsonTextReader reader = new JsonTextReader(streamReader))
            {
                reader.SupportMultipleContent = true;
                reader.Read();
                int records = 0;
                int totalCellsInt = 0;
                while (reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        reader.Read();
                    }
                    
                    if (records == 0)
                    {
                        var normalization = ReadPropertyValue(reader, "Normalization");
                        var totalCells = ReadPropertyValue(reader, "Length");

                        if (!int.TryParse(totalCells, out totalCellsInt))
                        {
                            throw new FormatException("Failed to parse cell count value: " + totalCells);
                        }
                    }

                    if (reader.TokenType != JsonToken.PropertyName)
                    {
                        throw new FormatException("Expected a property name, got: " + reader.TokenType);
                    }

                    string propertyName = reader.Value.ToString();
                    if (!int.TryParse(propertyName, out _))
                    {
                        throw new FormatException("Failed to parse index: " + propertyName);
                    }

                    ReadStart(reader);
                    ReadPropertyValidate(reader, "Vector");

                    if (reader.TokenType != JsonToken.StartObject)
                    {
                        throw new FormatException("Expected a start of array, got: " + reader.TokenType);
                    }

                    reader.Read();
                    List<VectorCell> cells = new List<VectorCell>();
                    while (reader.TokenType != JsonToken.EndObject)
                    {
                        ReadProperty(reader);
                        var cellName = reader.Value.ToString();
                        int index = 0;
                        string value;
                        string indexText;
                        if (!useName)
                        {
                            indexText = cellName;
                            reader.Read();
                            value = reader.Value.ToString();
                            reader.Read();
                        }
                        else
                        {
                            ReadStart(reader);
                            indexText = ReadPropertyValue(reader, "Index");
                            value = ReadPropertyValue(reader, "Value");
                            ReadEnd(reader);
                        }

                        if (!int.TryParse(indexText, out index))
                        {
                            throw new FormatException("Failed to parse cell index: " + index);
                        }

                        if (!double.TryParse(value, out var cellValue))
                        {
                            throw new FormatException("Failed to parse cell value: " + value);
                        }

                        cells.Add(new VectorCell(index, new SimpleCell(cellName, cellValue), 1));
                        
                    }

                    yield return new VectorData(cells.ToArray(), totalCellsInt, NormalizationType.None);
                    records++;
                    reader.Read();
                    ReadEnd(reader);
                }

                reader.Read();
            }
        }

        private static void ReadEnd(JsonTextReader reader)
        {
            if (reader.TokenType != JsonToken.EndObject)
            {
                throw new FormatException("Expected a start of array, got: " + reader.TokenType);
            }

            reader.Read();
        }

        private static void ReadStart(JsonTextReader reader)
        {
            reader.Read();
            if (reader.TokenType != JsonToken.StartObject)
            {
                throw new FormatException("Expected a start of array, got: " + reader.TokenType);
            }

            reader.Read();
        }

        private void ReadProperty(JsonTextReader reader)
        {
            if (reader.TokenType != JsonToken.PropertyName)
            {
                throw new FormatException("Expected a property name, got: " + reader.TokenType);
            }
        }

        private void ReadPropertyValidate(JsonTextReader reader, string name)
        {
            ReadProperty(reader);
            var propertyName = reader.Value.ToString();
            reader.Read();
            if (propertyName != name)
            {
                throw new FormatException("Expected Vector property but got:" + propertyName);
            }
        }

        private string ReadPropertyValue(JsonTextReader reader, string name)
        {
            ReadPropertyValidate(reader, name);
            var value = reader.Value.ToString();
            reader.Read();
            return value;
        }
    }
}
