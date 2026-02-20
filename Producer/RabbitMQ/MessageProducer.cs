using System.Text.Json;
using Producer.RabbitMQ.Connection;
using RabbitMQ.Client;

namespace Producer.RabbitMQ;

public class MessageProducer : IMessageProducer
{
    private readonly IRabbitMqConnection _connection;
    private readonly string _queueName;

    public MessageProducer(IRabbitMqConnection connection, string queueName)
    {
        _connection = connection;
        _queueName = queueName;
    }
    public async Task Send<T>(T message)
    {
        using var channel = await _connection.Connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _queueName, 
            durable: false, 
            exclusive: false, 
            autoDelete: false);

        var body = JsonSerializer.SerializeToUtf8Bytes(message);

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: _queueName,
            body: body);
    }
}
