using StackExchange.Redis;

namespace RedisLua
{
    public interface IRedisConnectionManager
    {
	    IDatabase RedisDatabase { get; }
    }
}
