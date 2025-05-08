using UnityEngine;

public class CardItem : MonoBehaviour
{
    private PokerGate gate;

    void Start()
    {
        gate = FindObjectOfType<PokerGate>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gate != null)
        {
            gate.CollectCard(gameObject);
        }
    }
}
