using UnityEngine;

public class PlayerModelTweening : MonoBehaviour
{
    [SerializeField] private Transform model = null; // can make this an array to do multiple pieces

    [Header("Customizables")]
    [SerializeField] private float tiltScale = 2f;
    [SerializeField] private float bobDistance = 1f,
                                   bobSpeed = 5f;

    private Rigidbody rb;
    private float baseY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        baseY = model.position.y;
    }

    private void Update()
    {
        HandleBob();
        HandleTilt();
    }

    private void HandleBob()
    {
        Vector3 newPos = new Vector3(
            model.position.x,
            Mathf.Lerp(baseY - bobDistance, baseY + bobDistance, Mathf.Abs(Mathf.Sin(Time.time * bobSpeed))),
            model.position.z);

        model.position = newPos;
    }

    private void HandleTilt()
    {
        model.rotation = Quaternion.Euler(new Vector3(
            rb.velocity.x * tiltScale * transform.forward.x
           + rb.velocity.z * tiltScale * transform.right.x,

            model.eulerAngles.y, // leave [Britne]y alone -> looks at mouse

            rb.velocity.z * -tiltScale * transform.right.z
           + rb.velocity.x * -tiltScale * transform.forward.z
            ));
    }
}