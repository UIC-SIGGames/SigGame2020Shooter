using UnityEngine;
using System;

[SelectionBase]
public class Player : MonoBehaviour
{
    [Header("Death Properties")]
    [SerializeField] private float explosionRadius = 20f;
    [SerializeField] private float explosiveForce = 1000f,
                                   maxTorqueMultiplier = 50f;
    
    private Rigidbody rb;
    private iWeapon weapon; // maybe have max 2 weapons - l/r click? round robin replace?

    private void Start()
    {
        weapon = GetComponentInChildren<iWeapon>();
        rb = GetComponent<Rigidbody>();
        GameManager.OnEnd += Die;
    }

    private void OnDestroy()
    {
        GameManager.OnEnd -= Die;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            weapon?.CommandFire();
    }

    private void Die()
    {
        Destroy(GetComponent<PlayerModelTweening>());
        Destroy(GetComponent<PlayerMovement>());

        BlowUp();

        Destroy(this);
    }

    private void BlowUp()
    {
        Instantiate(Resources.Load<GameObject>("Explosion"), transform.position, Quaternion.identity); // pls replace this effect

        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;

        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in objectsInRadius)
        {
            if (collider.attachedRigidbody)
            {
                collider.GetComponent<iHealth>()?.TakeDamage(1000, null);
                collider.attachedRigidbody.AddExplosionForce(explosiveForce, transform.position, explosionRadius);
                Vector3 torque = collider.attachedRigidbody.velocity * UnityEngine.Random.Range(maxTorqueMultiplier / 5, maxTorqueMultiplier);
                collider.attachedRigidbody.AddTorque(torque, ForceMode.Impulse);
            }
        }
    }
}   