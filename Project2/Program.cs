using Confluent.Kafka;
using Project2.Tools;

var topic = Environment.GetEnvironmentVariable("Kafka__TOPIC") ?? throw new Exception("Missing environment variable: Kafka__TOPIC");

var consumer = DateConsumerFactory.Create();
consumer.Subscribe(topic);

while (true)
{
    try
    {
        var result = consumer.Consume(CancellationToken.None);
        if (result is null)
            throw new NullReferenceException("Consumer did not receive message");

        if (result.Message.Value.IsCorrupted())
        {
            Console.WriteLine($"Message has been corrupted or it's data is damaged");
        }
        else
        {
            Console.WriteLine($"{result.Message.Value.GetDate()}");
            consumer.Commit(result);
        }
    }
    catch (KafkaException ex)
    {
        Console.WriteLine($"An error occured consuming the message: {ex.Message}");
        break;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An internal app error has occured: {ex.Message}");
        break;
    }
}