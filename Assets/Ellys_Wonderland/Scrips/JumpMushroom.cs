using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMushroom : MonoBehaviour
{
    public float jumpHeight = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float gravity = Physics2D.gravity.y * rb.gravityScale;
                float jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * jumpHeight);

                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            }
        }
    }
}

