using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    public HeartQueen boss; // 하트여왕 스크립트 연결 필요

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 위에서 떨어졌는지 체크 (예: y 속도가 음수인지)
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && rb.velocity.y < 0)
            {
                boss.TakeStompDamage(); // 데미지 주기
                rb.velocity = new Vector2(rb.velocity.x, 10f); // 튕겨 오르게 하기 (옵션)
            }
        }
    }
}
