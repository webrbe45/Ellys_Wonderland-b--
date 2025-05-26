using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public float lowJumpMultiplier = 2f;
    public float fallMultiplier = 2.5f;
    public LayerMask whatIsGround;
    public float bouncepower = 30f;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        isGrounded = Physics2D.OverlapCircle(transform.position + Vector3.down * 0.5f, 0.2f, whatIsGround);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed, 0, 0);
            anim.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
            anim.SetFloat("yVelocity", rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed, 0, 0);
            anim.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
            anim.SetFloat("yVelocity", rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("IsJumping", !isGrounded);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpPad"))
        {

            rb.velocity = new Vector2(rb.velocity.x, 0); 
            rb.AddForce(Vector2.up * bouncepower, ForceMode2D.Impulse);
            anim.SetBool("IsJumping", !isGrounded);
        }
        if (collision.gameObject.CompareTag("HeartQueen"))
        {
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 playerCenter = transform.position;
            if (contactPoint.y < playerCenter.y - 0.1f)
            {
                HeartQueen queen = collision.gameObject.GetComponent<HeartQueen>();
                if (queen != null)
                {
                    queen.TakeStompDamage();

                    Rigidbody2D rb = GetComponent<Rigidbody2D>();
                    rb.velocity = new Vector2(rb.velocity.x, 10f);
                }
            }
        }
    }
        public PokerGate gate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("O") || collision.CompareTag("X"))
        {
            FindObjectOfType<PokerGate>().CollectCard(collision.gameObject);
        }
    }
}
