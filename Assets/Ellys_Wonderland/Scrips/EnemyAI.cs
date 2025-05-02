using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f; //�⺻ �̵��ӵ�
    public float chaseSpeed = 5f; //���� �ӵ�
    public float patrolDistance = 3f; //�¿� �̵��Ÿ�
    public float detectionRange = 5f; //�÷��̾� ���� ����

    private Vector3 startPos;
    private bool movingRight = true;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
        }
        else
        {
            Patrol();            
        }
    }

    void Patrol()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(patrolSpeed, rb.velocity.y);
            if (transform.position.x >= startPos.x + patrolDistance)
                movingRight = false;
        }
        else
        {
            rb.velocity = new Vector2(-patrolSpeed, rb.velocity.y);
            if (transform.position.x <= startPos.x - patrolDistance)
                movingRight = true;
        }
    }
}
