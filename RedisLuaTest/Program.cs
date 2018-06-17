using System;
using System.Diagnostics;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace RedisLua
{
    class Program
    {
	    private static int _totalSoldiers = 50000;
	    private static readonly Random Rand = new Random();
		private static readonly RedisConnectionManager RedisConnection = new RedisConnectionManager("localhost");

		private static readonly string GetListOfSoldiers = (@" 
                local retArray = {} 
				local keys = (redis.call('keys', ARGV[1]))

				for i,key in ipairs(keys) do 
					retArray[i] = redis.call('GET', key)
					i=i+1
				end

				return retArray
				");

	    static void Main(string[] args)
        {
	        //GenerateData();

	        var stopWatch = new Stopwatch();
			stopWatch.Start();

			var redisValueArgs = new RedisValue[] { "SX100Y100_*" };
			var rowDataFromRedis = RedisConnection.RedisDatabase.ScriptEvaluate(GetListOfSoldiers, null, redisValueArgs);

	        var rowDataAsRedisResult = (RedisResult)rowDataFromRedis;
	        var scriptReturnValueArray = (RedisResult[])rowDataAsRedisResult;

			stopWatch.Stop();
			

	        var results = scriptReturnValueArray;
	        Console.WriteLine(results.Length + $" Time:{stopWatch.ElapsedMilliseconds}");
	        foreach (var item in results)
	        {
				var decoded = JsonConvert.DeserializeObject<Soldier>(item.ToString());
		        Console.WriteLine(decoded.X + "," + decoded.Y);
	        }
	        Console.ReadKey();
		}

		private static void GenerateData()
	    {
			for (var i = 0; i < _totalSoldiers; i++)
		    {
			    var value = new Soldier
			    {
				    X = Rand.Next(0, 1000),
				    Y = Rand.Next(0, 1000)
			    };

			    var uniqueNumber = i.ToString().PadLeft(5, '0');
			    var key = $"{value.BlockAddress}_{uniqueNumber}";

			    RedisConnection.RedisDatabase.StringSet(key, JsonConvert.SerializeObject(value));
		    }
		}
    }
}
