using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float accelForce = 5f;

    private Rigidbody rb;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleRotation();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private Vector3 direction;
    private float angle;
    private void HandleRotation()
    {
        direction = Input.mousePosition - mainCam.WorldToScreenPoint(transform.position);
        angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
    }

    private Vector3 moveInput;
    private void HandleMovement()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveInput = Vector3.ClampMagnitude(moveInput, 1); // Prevents fast diagonal acceleration

        if (moveInput.magnitude > 0)
            rb.AddForce(moveInput * accelForce, ForceMode.Acceleration);
    }
}