using RabbitMQ.Client;

namespace Producer.RabbitMQ.Connection;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}