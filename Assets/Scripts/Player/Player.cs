using System.Collections;
using UnityEngine;
using Cinemachine;

[SelectionBase]
public class Player : MonoBehaviour
{
    [Header("Damaged Properties")]
    [SerializeField] private float damageAmount = 30f;
    [SerializeField] private float recoveryTime = 0.3f, 
                                   screenShakeAmount = 5f,
                                   shuntForce = 3f;

    [Header("Death Properties")]
    [SerializeField] private float explosionRadius = 20f;
    [SerializeField] private float explosiveForce = 1000f,
                                   maxTorqueMultiplier = 50f;

    private bool takingHits = true;
    
    private Rigidbody rb;
    private iWeapon weapon; // maybe have max 2 weapons - l/r click? round robin replace?
    private CinemachineImpulseSource impulseSource;

    private void Start()
    {
        weapon = GetComponentInChildren<iWeapon>();
        rb = GetComponent<Rigidbody>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        GameManager.OnEnd += Die;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            weapon?.CommandFire();
    }

    private IEnumerator TakeHit(Vector3 normal)
    {
        impulseSource.GenerateImpulse(Vector3.one * screenShakeAmount);
        Shunt(normal);
        takingHits = false;
        // add a blinking effect on the player model
        BatteryManager.Instance?.RemoveEnergy(damageAmount);
        yield return new WaitForSeconds(recoveryTime);
        takingHits = true;
    }

    private void Shunt(Vector3 normal)
    {
        rb.velocity += (normal * shuntForce);
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
        // instantiate explosion effect

        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;

        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in objectsInRadius)
        {
            if (collider.attachedRigidbody)
            {
                collider.GetComponent<iHealth>()?.TakeDamage(1000, null);
                collider.attachedRigidbody.AddExplosionForce(explosiveForce, transform.position, explosionRadius);
                Vector3 torque = collider.attachedRigidbody.velocity * Random.Range(maxTorqueMultiplier / 5, maxTorqueMultiplier);
                collider.attachedRigidbody.AddTorque(torque, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.GetComponent<iEnemy>().IsDead() && takingHits)
        {
            StartCoroutine(TakeHit(collision.contacts[0].normal));
        }
    }
}   