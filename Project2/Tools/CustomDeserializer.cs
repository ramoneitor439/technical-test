using System.Text.Json;
using Confluent.Kafka;
using Project2.Models;

namespace Project2.Tools;

public class CustomDeserializer : IDeserializer<DateMessage>
{
    public DateMessage Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if(data.IsEmpty)
            throw new ArgumentNullException(nameof(data));
        
        var jsonString = System.Text.Encoding.UTF8.GetString(data);
        return JsonSerializer.Deserialize<DateMessage>(jsonString) ?? throw new NullReferenceException("DateMessage deserialization failed");
    }
}
