using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("â�� �浹��: " + collision.name);

        // Player �±׸� ���� ������Ʈ���� ����
        if (collision.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� ����!");
            // ��: �÷��̾� ü�� ���� �ڵ� �߰� ����
            Destroy(gameObject); // ���� �÷��̾�� �浹���� ���� â ����
        }
    }
}
