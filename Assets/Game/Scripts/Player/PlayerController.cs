using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public float deceleration = 5f;

    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode attackKey = KeyCode.Space;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 currentVelocity;
    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; 

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey) && !isAttacking)
        {
            Attack();
        }

        verticalInput = 0f;
        if (Input.GetKey(upKey)) verticalInput = 1f;
        if (Input.GetKey(downKey)) verticalInput = -1f;

        horizontalInput = 0f;
        if (Input.GetKey(leftKey)) horizontalInput = 1f;
        if (Input.GetKey(rightKey)) horizontalInput = -1f;
    }

    void FixedUpdate()
    {
        float rotation = horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, 0, rotation);

        Vector2 targetVelocity = transform.up * verticalInput * speed;

        if (verticalInput != 0)
        {
            currentVelocity = targetVelocity;
        }
        else
        {
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }

        rb.linearVelocity = currentVelocity;

        float movementSpeed = currentVelocity.magnitude;

        if (movementSpeed < 0.05f)
        {
            movementSpeed = 0f;
        }

        animator.SetFloat("Speed", movementSpeed);
    }

    void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
    }

    public void EndAttack()
    {
        isAttacking = false;
    }
}
