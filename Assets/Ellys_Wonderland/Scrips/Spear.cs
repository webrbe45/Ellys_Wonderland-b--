using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("â ����!");
        if (collision.CompareTag("Player"))
        {

            Debug.Log("�÷��̾�� ����!");
            // �÷��̾� ü�� ���� �� ó��
        }

        Destroy(gameObject); // �浹 �� â ����
    }


}
