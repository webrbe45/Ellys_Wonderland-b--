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
    public float bouncepower = 30f;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private float horizontalInput;
    private bool facingRight = true; // 기본적으로 오른쪽을 바라봄

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        // 더 안정적인 땅 감지 시스템
        CheckGrounded();

        //입력 받기
        horizontalInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
            if (facingRight)
            {
                Flip();
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
            if (!facingRight)
            {
                Flip();
            }
        }

        // 점프 입력 (Space 키로 직접 체크)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        //점프 물리 조정
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //애니메이션 상태 업데이트
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        //물리 기반 이동
        if(horizontalInput != 0)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }
        else
        {
            // 입력이 없을 때 즉시 멈춤
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    void UpdateAnimations()
    {
        // 점프 중인지 확인 (땅에 닿지 않았을 때만)
        if (!isGrounded)
        {
            anim.SetBool("IsJumping", true);
            anim.SetBool("IsMoving", false);
            anim.SetBool("IsIdle", false);
        }
        // 이동 중인지 확인 (땅에 닿고 실제 입력이 있을 때)
        else if (isGrounded && Mathf.Abs(horizontalInput) > 0.1f)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsMoving", true);
            anim.SetBool("IsIdle", false);
        }
        // 정지 상태 (땅에 닿고 입력이 없을 때)
        else if (isGrounded)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsMoving", false);
            anim.SetBool("IsIdle", true);
        }
    }

    void CheckGrounded()
    {
        Vector2 rayOrigin = (Vector2)transform.position + Vector2.down * 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 0.1f);

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpPad"))
        {

            rb.velocity = new Vector2(rb.velocity.x, 0); 
            rb.AddForce(Vector2.up * bouncepower, ForceMode2D.Impulse);
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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
