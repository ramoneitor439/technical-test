using Confluent.Kafka;
using Project1.Models;
using Project1.Tools;

var topic = Environment.GetEnvironmentVariable("Kafka__TOPIC") ?? throw new Exception("Missing environment variable: Kafka__TOPIC");

using var producer = DateProducerFactory.Create();

while (true)
{
    var message = new DateMessage(DateTime.Now);

    try
    {
        var result = await producer.ProduceAsync(topic, new Message<string, DateMessage>
        {
            Key = Guid.NewGuid().ToString(),
            Value = message,
        });

        Console.WriteLine($"Message produced successfully: Topic: {topic}, Key: {result.Key}, Status: {result.Status}");
    }
    catch (ProduceException<string, DateMessage> ex)
    {
        Console.WriteLine(ex.Error.Reason);
        break;
    }
    catch (KafkaException ex)
    {
        Console.WriteLine($"An error occured producing the message: {ex.Message}");
        break;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An internal app error has occured: {ex.Message}");
        break;
    }
    
    await Task.Delay(TimeSpan.FromSeconds(1));
}

producer.Flush(TimeSpan.FromSeconds(10));

Console.WriteLine("Closing connection...");
Console.WriteLine("App closed");