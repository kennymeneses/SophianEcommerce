using StackExchange.Redis;

namespace Ecommerce.Tools
{
    public class RedisConnection
    {
        private readonly ConnectionMultiplexer _connection;
        ConfigurationOptions option = new ConfigurationOptions
        {
            AbortOnConnectFail = false,
            EndPoints = {"localhost:6379"}
        };

        public RedisConnection()
        {
            _connection = ConnectionMultiplexer.Connect(option);
        }

        public IDatabase Database => _connection.GetDatabase();
    }
}
