using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;

    private Rigidbody2D rigidbodyGoombo;
    private Vector2 velocity;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidbodyGoombo = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        
            enabled = false;
    }

    private void OnEnable()
    {
        rigidbodyGoombo.WakeUp();
    }

    private void OnDisable()
    {
        rigidbodyGoombo.velocity = Vector2.zero;
        rigidbodyGoombo.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y* Time.fixedDeltaTime;

        rigidbodyGoombo.MovePosition(rigidbodyGoombo.position + velocity * Time.fixedDeltaTime);

        if (rigidbodyGoombo.Raycast(direction))
        {
            direction = -direction;
            if (spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            } else
            {
                spriteRenderer.flipX = true;
            }
        }

        if (rigidbodyGoombo.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }
}
