using System.Collections;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private PlayerBlink blink;

    void Start()
    {
        blink = GetComponent<PlayerBlink>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.tag == "Enemy" || collision.transform.tag == "Trap") && !PlayerManager.isGameOver)
        {
            Vector3 hitSource = collision.transform.position;
            Vector2 knockbackDir = (transform.position - hitSource).normalized;

            // �˹� ȿ�� + ���� ó��
            blink.TriggerBlink(knockbackDir);

            // ü�� ����
            HealthManager.health--;
            if (HealthManager.health <= 0)
            {
                PlayerManager.isGameOver = true;
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(GetHurt());
            }
        }
    }

    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 8);
        yield return new WaitForSeconds(3f);
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
}
