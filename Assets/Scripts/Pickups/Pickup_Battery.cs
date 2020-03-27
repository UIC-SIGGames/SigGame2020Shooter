using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup_Battery : MonoBehaviour, iPickup
{
    private float magnetSpeed = 1f;
    private bool magnetized = false;

    private Transform magnetTarget;
    private Rigidbody rb;

    public event Action OnPickup = delegate { };

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 force = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(1f, 2f), UnityEngine.Random.Range(-1f, 1f));
        rb.AddForce(force * UnityEngine.Random.Range(1, 5), ForceMode.Impulse);
        ScoreManager.Instance?.TrackBatteries(false);
    }

    private void Update()
    {
        if (magnetized)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, magnetTarget.position, magnetSpeed * Time.deltaTime));
            magnetSpeed *= 1.05f; // nice effect, discriminates against low framerates as pickups don't have as many frames to accelerate.
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BatteryManager.Instance?.AddEnergy(GameManager.BatPickupBonus);
            Instantiate(Resources.Load<GameObject>("Pickup Particles"), transform.position, Quaternion.identity); // recycle
            ScoreManager.Instance?.TrackBatteries(true);

            transform.position = new Vector3(0, -100, 0); // bad bad bad ALL OF THIS IS BAD
            OnPickup();
            magnetized = false; // use a global sound manager PlayOneShot instead of doing these 4 lines??
            Destroy(gameObject, 0.2f); // most DEFINITELY recycle these instead
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
