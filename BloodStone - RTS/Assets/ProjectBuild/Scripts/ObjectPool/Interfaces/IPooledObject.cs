namespace Pool
{
    public interface IPooledObject
    {
        IPoolObject PoolObject { get; }
    }
}