using System.Text.Json.Serialization;

namespace MaerskChallenge.Model
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum JobStatus
    {
        Pending,
        Completed,
        Failed
    }
}
