namespace RythmGame
{
    public interface ICollectable<T>
    {
        public delegate void OnCollectEvent(T content);
    }
}
