using System;
using StackExchange.Redis;

namespace RedisLua
{
    public class RedisConnectionManager : IRedisConnectionManager
    {
	    private static string _redisServerAddress;

	    public RedisConnectionManager(string redisServerAddress)
	    {
		    _redisServerAddress = redisServerAddress;
	    }

	    private static readonly Lazy<ConfigurationOptions> ConfigOptions = new Lazy<ConfigurationOptions>(() =>
	    {
		    var configOptions = new ConfigurationOptions();
		    configOptions.EndPoints.Add(_redisServerAddress);
		    configOptions.ClientName = "RedisConnection";
		    configOptions.ConnectTimeout = 100000;
		    configOptions.SyncTimeout = 100000;
		    configOptions.AbortOnConnectFail = false;
		    return configOptions;
	    });

	    private readonly Lazy<ConnectionMultiplexer> _connectionMux = new Lazy<ConnectionMultiplexer>(
		    () => ConnectionMultiplexer.Connect(ConfigOptions.Value));

	    public IDatabase RedisDatabase => _connectionMux.Value.GetDatabase(0);
    }
}
