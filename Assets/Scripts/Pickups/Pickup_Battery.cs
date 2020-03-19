using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup_Battery : MonoBehaviour
{
    private float magnetSpeed = 1f;
    private bool magnetized = false;

    private Transform magnetTarget;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 force = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f));
        rb.AddForce(force * Random.Range(1, 5), ForceMode.Impulse);
    }

    private void Update()
    {
        if (magnetized)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, magnetTarget.position, magnetSpeed * Time.deltaTime));
            magnetSpeed *= 1.05f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BatteryManager.Instance?.AddEnergy(GameManager.BatPickupBonus);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            magnetTarget = other.transform;
            magnetized = true;
        }
    }
}
