using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    public HeartQueen boss; // ��Ʈ���� ��ũ��Ʈ ���� �ʿ�

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾ ������ ���������� üũ (��: y �ӵ��� ��������)
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && rb.velocity.y < 0)
            {
                boss.TakeStompDamage(); // ������ �ֱ�
                rb.velocity = new Vector2(rb.velocity.x, 10f); // ƨ�� ������ �ϱ� (�ɼ�)
            }
        }
    }
}
