using System.Text.Json;
using Producer.RabbitMQ.Connection;
using RabbitMQ.Client;

namespace Producer.RabbitMQ;

public class MessageProducer : IMessageProducer
{
    private readonly IRabbitMqConnection _connection;

    public MessageProducer(IRabbitMqConnection connection)
    {
        _connection = connection;
    }
    public async Task Send<T>(T message, string queueName)
    {
        using var channel = await _connection.Connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: queueName, 
            durable: false, 
            exclusive: false, 
            autoDelete: false);

        var body = JsonSerializer.SerializeToUtf8Bytes(message);

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queueName,
            body: body);
    }
}
