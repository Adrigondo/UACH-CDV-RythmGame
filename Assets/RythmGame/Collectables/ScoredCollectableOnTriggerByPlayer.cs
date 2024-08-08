using UnityEngine;

namespace RythmGame
{
    public class ScoredCollectableOnTriggerByPlayer : MonoBehaviour, ICollectableOnTrigger<float>
    {
        private float _scoreValue;

        [SerializeField]
        protected float ScoreValue
        {
            get
            {
                return _scoreValue;
            }
            set
            {
                _scoreValue = value;
            }
        }

        public ICollectableOnTrigger<float>.OnCollectEvent OnCollectEvent;

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnCollectEvent?.Invoke(ScoreValue);
                Dissappear();
            }
        }
        public virtual void Dissappear()
        {
            gameObject.SetActive(false);
        }
    }
}