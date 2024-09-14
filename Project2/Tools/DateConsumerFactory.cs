using Confluent.Kafka;
using Project2.Models;

namespace Project2.Tools;

public static class DateConsumerFactory
{
    public static IConsumer<string, DateMessage> Create()
    {
        var bootstrapServer = Environment.GetEnvironmentVariable("Kafka__BOOTSTRAP_SERVER") ?? throw new Exception("Missing environment variable: Kafka__BOOTSTRAP_SERVER");
        var config = new ConsumerConfig
        {
            BootstrapServers = bootstrapServer,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,
            GroupId = Environment.GetEnvironmentVariable("Kafka__GROUP_ID") ?? throw new Exception("Missing environment variable: Kafka__GROUP_ID"),
        };
        
        var consumer = new ConsumerBuilder<string, DateMessage>(config)
            .SetValueDeserializer(new CustomDeserializer())
            .Build();

        return consumer;
    }
}