using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public float blinkDuration = 2f;
    public float blinkInterval = 0.1f;
    public float knockbackForce = 5f;
    public float damageCooldown = 2f;

    private float damageTimer = 0f;
    private bool isBlinking = false;
    private bool isTouchingTrap = false;
    private GameObject currentTrap;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isTouchingTrap && damageTimer <= 0 && !isBlinking)
        {
            Vector2 direction = (transform.position - currentTrap.transform.position).normalized;
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            StartCoroutine(BlinkEffect());
            damageTimer = damageCooldown;
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

    private System.Collections.IEnumerator BlinkEffect()
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
}
