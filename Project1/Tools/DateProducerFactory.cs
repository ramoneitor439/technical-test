using Confluent.Kafka;
using Project1.Models;

namespace Project1.Tools;

public static class DateProducerFactory
{
    public static IProducer<string, DateMessage> Create()
    {
        var bootstrapServer = Environment.GetEnvironmentVariable("Kafka__BOOTSTRAP_SERVER") ?? throw new Exception("Missing environment variable: Kafka__BOOTSTRAP_SERVER");
        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServer,
            Acks = Acks.All,
        };
        
        var producer = new ProducerBuilder<string, DateMessage>(config)
            .SetValueSerializer(new CustomSerializer<DateMessage>())
            .Build();
        
        return producer;
    }
}