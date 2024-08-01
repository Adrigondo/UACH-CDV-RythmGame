using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    [SerializeField] protected ScoreManager scoreManager;

    void Start()
    {
        if (scoreManager == null)
        {
            Debug.LogError("No Score Manager found on Coin");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            scoreManager.ReceiveCollectableTag(this.tag);
        }
    }
}
