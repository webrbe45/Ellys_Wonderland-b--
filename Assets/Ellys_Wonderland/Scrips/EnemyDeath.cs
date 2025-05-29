using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float bounceForce = 5f; // 플레이어가 밟았을 때 튕겨 오르는 힘
    private Animator anim;
    private bool isDead = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

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

                    isDead = true;
                    anim.SetTrigger("IsDie"); // ← 애니메이션 트리거 발동

                    // Destroy는 조금 늦게 실행 (애니메이션 다 보고 제거)
                    Destroy(gameObject, 1.0f); // 애니메이션 길이에 맞춰 시간 조절

                }
            }
        }
    }
}
