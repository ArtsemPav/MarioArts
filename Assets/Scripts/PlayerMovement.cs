using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rigidbodyMario;
    private Vector2 velocity;
    private float inputAxis;

    public float MoveSpeed = 8f;

    private void Awake()
    {
        rigidbodyMario = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HorizontalMoving();
    }

    private void HorizontalMoving()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * MoveSpeed, MoveSpeed * Time.deltaTime);
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
}
