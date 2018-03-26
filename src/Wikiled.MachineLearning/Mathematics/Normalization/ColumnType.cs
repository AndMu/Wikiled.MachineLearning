using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wikiled.MachineLearning.Mathematics.Normalization
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ColumnType
    {
        Numeric,
        Categorical
    }
}
