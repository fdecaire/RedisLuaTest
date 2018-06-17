using RedisLua;
using Xunit;

namespace RedisLuaUnitTests
{
    public class SoldierTests
    {
        [Fact]
        public void SoldierBlockAddress_35_30()
        {
	        var soldier = new Soldier
	        {
				X=35,
				Y=30
	        };

	        Assert.Equal("SX100Y100", soldier.BlockAddress);
        }
	    [Fact]
	    public void SoldierBlockAddress_500_380()
	    {
		    var soldier = new Soldier
		    {
			    X = 500,
			    Y = 380
		    };

		    Assert.Equal("SX600Y400", soldier.BlockAddress);
	    }
	    [Fact]
	    public void SoldierBlockAddress_0_0()
	    {
		    var soldier = new Soldier
		    {
			    X = 0,
			    Y = 0
		    };

		    Assert.Equal("SX100Y100", soldier.BlockAddress);
	    }
	    [Fact]
	    public void SoldierBlockAddress_100_100()
	    {
		    var soldier = new Soldier
		    {
			    X = 100,
			    Y = 100
		    };

		    Assert.Equal("SX200Y200", soldier.BlockAddress);
	    }
	    [Fact]
	    public void SoldierBlockAddress_99_99()
	    {
		    var soldier = new Soldier
		    {
			    X = 99,
			    Y = 99
		    };

		    Assert.Equal("SX100Y100", soldier.BlockAddress);
	    }
	}
}
