using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wikiled.MachineLearning.Normalization
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ColumnType
    {
        Numeric,
        Categorical
    }
}
