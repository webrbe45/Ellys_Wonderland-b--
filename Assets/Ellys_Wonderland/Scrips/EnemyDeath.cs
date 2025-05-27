using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float bounceForce = 5f; // �÷��̾ ����� �� ƨ�� ������ ��

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f)
                {
                    Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                    if (playerRb != null)
                    {
                        playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce); 
                    }

                    Destroy(gameObject);
                    
                }
            }
        }
    }
}
