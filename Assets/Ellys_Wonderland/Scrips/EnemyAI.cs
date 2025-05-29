using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f; //�⺻ �̵��ӵ�
    public float chaseSpeed = 5f; //���� �ӵ�
    public float patrolDistance = 2f; //�¿� �̵� �Ÿ�
    public float detectionRange = 10f; //�÷��̾� �ν� ����
    public float stopDuration = 1f; // �÷��̾�� �浹 �� ���ߴ� �ð�

    private Animator anim;

    private Transform player;
    private Vector2 startPos;
    private bool movingRight = true;
    private bool isChasing = false;
    private bool isStopped = false;
    private float stopTimer = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = transform.position;
    }

    void Update()
    {
        if (isStopped)
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0f)
            {
                isStopped = false;
            }
            return;
        }

        // player�� null���� Ȯ��
        if (player == null)
        {
            // player�� �ٽ� ã�ƺ���
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                // player�� ������ ��Ʈ�Ѹ� ����
                Patrol();
                return;
            }
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * chaseSpeed * Time.deltaTime);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {

        anim.SetBool("IsStop", true);
        anim.SetBool("IsGo", false);
        anim.SetBool("IsReady", false);
        float move = (movingRight ? 1 : -1) * patrolSpeed * Time.deltaTime;
        transform.Translate(move, 0, 0);

        if (Vector2.Distance(transform.position, startPos) > patrolDistance)
        {
            movingRight = !movingRight;

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (movingRight ? 1 : -1);
            transform.localScale = scale;
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
