using UnityEngine;
using System.Collections;

public class PlayerBlink : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public float blinkDuration = 2f;
    public float blinkInterval = 0.1f;
    public float knockbackForce = 5f;
    public float damageCooldown = 2f;
    public int maxHP = 3;

    private float damageTimer = 0f;
    private bool isBlinking = false;
    private bool isTouchingTrap = false;
    private GameObject currentTrap;
    public int currentHP;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }

    void Update()
    {
        //currentTrap != null 추가
        if (isTouchingTrap && currentTrap != null && damageTimer <= 0 && !isBlinking)
        {
            TakeDamage(1, currentTrap.transform.position);
        }

        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            isTouchingTrap = true;
            currentTrap = collision.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            isTouchingTrap = false;
            currentTrap = null;
        }
    }

    void TakeDamage(int amount, Vector3 damageSourcePosition)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Die();
            return;
        }

        Vector2 direction = (transform.position - damageSourcePosition).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(BlinkEffect());
        damageTimer = damageCooldown;
    }

    private IEnumerator BlinkEffect()
    {
        isBlinking = true;
        float timer = 0f;
        while (timer < blinkDuration)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }
        sr.enabled = true;
        isBlinking = false;
    }

    void Die()
    {
        // 플레이어가 사망할 때 모든 연결된 참조 정리
        StopAllCoroutines();
        isTouchingTrap = false;
        currentTrap = null;

        // 게임오브젝트 파괴
        Destroy(gameObject);
    }
}
