using System.Text.Json;
using Confluent.Kafka;

namespace Project1.Tools;

internal class CustomSerializer<T> : ISerializer<T>
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        if(data is null)
            throw new ArgumentNullException(nameof(data));

        var jsonString = JsonSerializer.Serialize(data);
        return System.Text.Encoding.UTF8.GetBytes(jsonString);
    }
}