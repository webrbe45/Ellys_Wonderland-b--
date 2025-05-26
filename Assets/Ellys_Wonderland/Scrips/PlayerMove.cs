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
    private bool facingRight = true; // �⺻������ �������� �ٶ�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        // �� �������� �� ���� �ý���
        CheckGrounded();

        //�Է� �ޱ�
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

        // ���� �Է� (Space Ű�� ���� üũ)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        //���� ���� ����
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //�ִϸ��̼� ���� ������Ʈ
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        //���� ��� �̵�
        if(horizontalInput != 0)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }
        else
        {
            // �Է��� ���� �� ��� ����
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    void UpdateAnimations()
    {
        // ���� ������ Ȯ�� (���� ���� �ʾ��� ����)
        if (!isGrounded)
        {
            anim.SetBool("IsJumping", true);
            anim.SetBool("IsMoving", false);
            anim.SetBool("IsIdle", false);
        }
        // �̵� ������ Ȯ�� (���� ��� ���� �Է��� ���� ��)
        else if (isGrounded && Mathf.Abs(horizontalInput) > 0.1f)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsMoving", true);
            anim.SetBool("IsIdle", false);
        }
        // ���� ���� (���� ��� �Է��� ���� ��)
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
