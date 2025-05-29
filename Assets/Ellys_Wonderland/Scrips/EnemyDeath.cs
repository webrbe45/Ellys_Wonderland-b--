using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float bounceForce = 5f; // ÇÃ·¹ÀÌ¾î°¡ ¹â¾ÒÀ» ¶§ Æ¨°Ü ¿À¸£´Â Èû
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
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
