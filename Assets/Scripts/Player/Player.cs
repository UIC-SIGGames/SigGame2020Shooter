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

    private void OnDestroy()
    {
        GameManager.OnEnd -= Die;
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
                Vector3 torque = collider.attachedRigidbody.velocity * Random.Range(maxTorqueMultiplier / 5, maxTorqueMultiplier);
                collider.attachedRigidbody.AddTorque(torque, ForceMode.Impulse);
            }
        }
    }

    // Using both to prevent unintended invulnerability
    // invulnerability was partially caused by bouncy material
    // but probably could've happened other ways too
    private void OnCollisionEnter(Collision collision) { HandleCollision(collision); }
    private void OnCollisionStay(Collision collision) { HandleCollision(collision); }

    private void HandleCollision(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<iEnemy>();

        if (enemy != null && !enemy.IsDead() && takingHits)
        {
            StartCoroutine(TakeHit(collision.contacts[0].normal));
        }
    }
}   