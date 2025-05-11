using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float fallSpeed = 5f;
    public GameObject warning; // ��� ������Ʈ ���� ����

    void Start()
    {
        // ��� ��� ������ �ı�
        if (warning)
        {
            Destroy(warning);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
