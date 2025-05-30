using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spear : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float patrolDistance = 10f;
    public float detectionRange = 20f;
    public float stopDuration = 1f;

    public GameObject spearPrefab; // 던질 창 프리팹
    public Transform throwPoint;   // 창이 나갈 위치
    public float throwForce = 20f; // 창 던지기 힘
    public float throwCooldown = 2f; // 창 던지기 쿨타임


    private Animator anim;

    private Transform player;
    private Vector2 startPos;
    private bool movingRight = true;
    private bool isChasing = false;
    private bool isStopped = false;
    private float stopTimer = 0f;
    private float lastThrowTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        startPos = transform.position;
        anim = GetComponent<Animator>();
    }

    void Update()
    {


        if (isStopped)
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0f)
                isStopped = false;
            return;
        }

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        isChasing = distanceToPlayer < detectionRange;

        if (isChasing)
        {
            // 추격
           /* Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * chaseSpeed * Time.deltaTime); */

            // 창 던지기
            if (Time.time >= lastThrowTime + throwCooldown)
            {
                ThrowSpear();
                lastThrowTime = Time.time;
            }
        }
        else
        {
            // Patrol(); // 필요시 다시 활성화
        }

        void ThrowSpear()
        {
            if (spearPrefab == null || throwPoint == null)
            {
                Debug.LogWarning("프리팹이나 throwPoint가 연결 안 됐어요!");
                return;
            }

            anim.SetTrigger("IsRedCard_Throw");

            Vector2 direction = (player.position - throwPoint.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90f; // spear 이미지가 아래를 기본 방향으로 할 때

            GameObject spear = Instantiate(spearPrefab, throwPoint.position, Quaternion.Euler(0, 0, angle));
            Debug.Log("창 생성됨, 회전각: " + angle);

            Rigidbody2D rb = spear.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * throwForce;
            }
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isStopped = true;
            stopTimer = stopDuration;
        }
    }
}
