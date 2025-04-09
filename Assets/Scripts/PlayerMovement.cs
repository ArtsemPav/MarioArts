using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rigidbodyMario;
    private Collider2D colliderMario;

    private Vector2 velocity;
    private float inputAxis;

    public float MoveSpeed = 8f;
    public float MaxJumpHeight = 5f;
    public float MaxJumpTime = 1f;

    public float JumpForce => (2f * MaxJumpHeight) / (MaxJumpTime / 2f);
    public float Gravity => (-2f * MaxJumpHeight) / Mathf.Pow((MaxJumpTime / 2f), 2);
    public bool Grounded { get; private set; }
    public bool Jumping { get; private set; }
    public bool Running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool Sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);

    private void Awake()
    {
        rigidbodyMario = GetComponent<Rigidbody2D>();
        colliderMario = GetComponent<Collider2D>();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        rigidbodyMario.isKinematic = false;
        colliderMario.enabled = true;
        velocity = Vector2.zero;
        Jumping = false;
    }

    private void OnDisable()
    {
        rigidbodyMario.isKinematic = true;
        colliderMario.enabled = false;
        velocity = Vector2.zero;
        Jumping = false;
    }

    private void Update()
    {
        HorizontalMoving();

        Grounded = rigidbodyMario.Raycast(Vector2.down);

        if (Grounded)
        {
            GroundedMovement();
        }

        ApplyGravity();
    }

    private void HorizontalMoving()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * MoveSpeed, MoveSpeed * Time.deltaTime);
        if (rigidbodyMario.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        } else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        velocity.y += Gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, Gravity / 2f);
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        Jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = JumpForce;
            Jumping = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbodyMario.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = mainCamera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x);

        rigidbodyMario.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = JumpForce / 2f;
                Jumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
           if (transform.DotTest(collision.transform,Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }
}
