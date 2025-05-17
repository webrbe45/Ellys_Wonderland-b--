using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpPower = 10f;
    public float lowJumpMultiplier = 2f;
    public float fallMultiplier = 2.5f;
    public float bouncepower = 30f;

    [Header("Ground Check")]
    public LayerMask whatIsGround;
    public float groundCheckRadius = 0.2f;
    public Vector2 groundCheckOffset = new Vector2(0, -0.5f);

    [Header("References")]
    public PokerGate pokerGate;

    private Rigidbody2D rb;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (pokerGate == null)
        {
            pokerGate = FindObjectOfType<PokerGate>();
        }
    }

    private void Update()
    {
        CheckGround();
        HandleInput();
        ApplyGravityModifiers();
    }

    private void CheckGround()
    {
        Vector2 groundCheckPosition = (Vector2)transform.position + groundCheckOffset;
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition, groundCheckRadius, whatIsGround);
    }

    private void HandleInput()
    {
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        transform.Translate(horizontalInput * speed * Time.deltaTime, 0, 0);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void ApplyGravityModifiers()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * bouncepower, ForceMode2D.Impulse);
            return;
        }

        if (collision.gameObject.CompareTag("HeartQueen"))
        {
            HandleHeartQueenCollision(collision);
        }
    }

    private void HandleHeartQueenCollision(Collision2D collision)
    {
        if (collision.contacts.Length == 0)
        {
            return;
        }

        Vector2 contactPoint = collision.contacts[0].point;
        Vector2 playerCenter = transform.position;

        if (contactPoint.y < playerCenter.y - 0.1f)
        {
            HeartQueen queen = collision.gameObject.GetComponent<HeartQueen>();
            if (queen != null)
            {
                queen.TakeStompDamage();
                rb.velocity = new Vector2(rb.velocity.x, 10f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("O") || collision.CompareTag("X"))
        {
            if (pokerGate != null)
            {
                pokerGate.CollectCard(collision.gameObject);
            }
            else
            {
                Debug.LogWarning("PokerGate reference is missing!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector2 groundCheckPosition = (Vector2)transform.position + groundCheckOffset;
        Gizmos.DrawWireSphere(groundCheckPosition, groundCheckRadius);
    }
}
