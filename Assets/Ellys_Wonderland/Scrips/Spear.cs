using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("창 던짐!");
        if (collision.CompareTag("Player"))
        {

            Debug.Log("플레이어에게 명중!");
            // 플레이어 체력 감소 등 처리
        }

        Destroy(gameObject); // 충돌 후 창 제거
    }


}
