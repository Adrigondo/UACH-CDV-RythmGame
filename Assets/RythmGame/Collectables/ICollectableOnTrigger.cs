using UnityEngine;

namespace RythmGame
{
    public interface ICollectableOnTrigger<T> : ICollectable<T>
    {
        public abstract void OnTriggerEnter2D(Collider2D other);
    }
}
