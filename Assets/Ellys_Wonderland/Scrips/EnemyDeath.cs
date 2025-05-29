using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float bounceForce = 5f; // �÷��̾ ����� �� ƨ�� ������ ��
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
                    anim.SetTrigger("IsDie"); // �� �ִϸ��̼� Ʈ���� �ߵ�

                    // Destroy�� ���� �ʰ� ���� (�ִϸ��̼� �� ���� ����)
                    Destroy(gameObject, 1.0f); // �ִϸ��̼� ���̿� ���� �ð� ����

                }
            }
        }
    }
}
