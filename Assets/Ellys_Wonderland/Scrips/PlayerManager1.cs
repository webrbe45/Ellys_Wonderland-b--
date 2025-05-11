using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager1 : MonoBehaviour
{
    [Header("체력 관련")]
    public int maxHealth = 5;
    public static int currentHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("무적 및 넉백")]
    public float blinkDuration = 2f;
    public float blinkInterval = 0.1f;
    public float knockbackForce = 5f;
    public float damageCooldown = 2f;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private float damageTimer = 0f;
    private bool isBlinking = false;
    private bool isTouchingTrap = false;
    private GameObject currentTrap;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        UpdateHearts();

        if (isTouchingTrap && currentTrap != null && damageTimer <= 0 && !isBlinking)
        {
            Vector2 direction = (transform.position - currentTrap.transform.position).normalized;
            TakeDamage(1, direction);
        }

        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }

    void TakeDamage(int amount, Vector2 knockbackDirection)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
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

    private bool isDead = false;

    void Die()
    {
        isDead = true;
        sr.enabled = false; 
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static; 
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
}
