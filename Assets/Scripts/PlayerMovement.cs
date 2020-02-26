using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float deceleration = 1f;

    private new Rigidbody rigidbody;
    private new Camera camera;

    private void Start()
    {
        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = moveVelocity;
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
    private Vector3 moveAccel;
    private Vector3 moveDecel;
    private void HandleMovement()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveInput = Vector3.ClampMagnitude(moveInput, 1); // Prevents fast diagonal acceleration

        moveAccel = moveInput * acceleration;
        moveVelocity += moveAccel;

        float xDir = Mathf.Sign(moveVelocity.x);
        float zDir = Mathf.Sign(moveVelocity.z);

        moveDecel = Vector3.zero;

        // Decelerate if no input but still moving
        if (moveAccel.x == 0 && moveVelocity.x != 0)
        {
            moveDecel.x += xDir * deceleration;
        }
        if (moveAccel.z == 0 && moveVelocity.z != 0)
        {
            moveDecel.z += zDir * deceleration;
        }

        moveDecel = Vector3.ClampMagnitude(moveDecel, deceleration); // Prevents fast diagonal deceleration

        moveVelocity -= moveDecel;

        // Clamps between -maxSpeed and 0 for negative velocities and 0 and maxSpeed for positive
        moveVelocity.x = Mathf.Clamp(moveVelocity.x, Mathf.Min(0, xDir * maxSpeed), Mathf.Max(0, xDir * maxSpeed));
        moveVelocity.z = Mathf.Clamp(moveVelocity.z, Mathf.Min(0, zDir * maxSpeed), Mathf.Max(0, zDir * maxSpeed));

        moveVelocity = Vector3.ClampMagnitude(moveVelocity, maxSpeed); // Prevents fast diagonal velocity
    }
}