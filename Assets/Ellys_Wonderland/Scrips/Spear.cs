using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("창이 충돌함: " + collision.name);

        // Player 태그를 가진 오브젝트에만 반응
        if (collision.CompareTag("Player"))
        {
            Debug.Log("플레이어에게 명중!");
            // 예: 플레이어 체력 감소 코드 추가 가능
            Destroy(gameObject); // 오직 플레이어와 충돌했을 때만 창 제거
        }
    }
}
