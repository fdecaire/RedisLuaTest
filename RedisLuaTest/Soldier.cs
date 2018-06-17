namespace RedisLua
{
    public class Soldier
    {
	    private readonly int _blockSize = 100;
		public int X { get; set; }
		public int Y { get; set; }

	    public string BlockAddress {
		    get
		    {
			    var x = (X / _blockSize + 1) * _blockSize;
			    var y = (Y / _blockSize + 1) * _blockSize;
			    return $"SX{x}Y{y}";
		    }
	    }  
    }
}
