using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private new Rigidbody rigidbody;
    private new Camera camera;

    private float sqrMaxVelocity;
    private void Start()
    {
        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
        sqrMaxVelocity = moveSpeed * moveSpeed;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = moveVelocity;

        // To prevent faster diagonal movement
        if (rigidbody.velocity.sqrMagnitude > sqrMaxVelocity)
            rigidbody.velocity = rigidbody.velocity.normalized * moveSpeed;
    }

    private Vector3 direction;
    private float angle;
    private void HandleRotation()
    {
        direction = Input.mousePosition - camera.WorldToScreenPoint(transform.position);
        angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
    }

    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private void HandleMovement()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;
    }
}
