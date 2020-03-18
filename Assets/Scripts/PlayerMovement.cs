using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float accelForce = 5f,
                  energyLossRate = 0.045f;

    private new Rigidbody rigidbody;
    private new Camera camera;

    private void Start()
    {
        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
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
        direction = Input.mousePosition - camera.WorldToScreenPoint(transform.position);
        angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
    }

    private Vector3 moveInput;
    private void HandleMovement()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveInput = Vector3.ClampMagnitude(moveInput, 1); // Prevents fast diagonal acceleration

        if (moveInput.magnitude > 0)
        {
            BatteryManager.Instance?.RemoveEnergy(energyLossRate);
            rigidbody.AddForce(moveInput * accelForce, ForceMode.Acceleration);
        }
    }
}