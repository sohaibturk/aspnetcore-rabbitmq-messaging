using RabbitMQ.Client;

namespace Producer.RabbitMQ.Connection;

public class RabbitMqConnection : IRabbitMqConnection, IDisposable
{
    private IConnection? _connection;
    public IConnection Connection => _connection!;

    private RabbitMqConnection(IConnection connection)
    {
        _connection = connection;
    }

    public static async Task<IRabbitMqConnection> CreateAsync(string hostName = "localhost")
    {
        var factory = new ConnectionFactory
        {
            HostName = hostName,
            //DispatchConsumersAsync = true,
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
        };

        var connection = await factory.CreateConnectionAsync();

        return new RabbitMqConnection(connection);
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }
}
