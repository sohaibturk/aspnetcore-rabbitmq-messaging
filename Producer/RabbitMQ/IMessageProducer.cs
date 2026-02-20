namespace Producer.RabbitMQ;

public interface IMessageProducer
{
    Task Send<T>(T message);
}