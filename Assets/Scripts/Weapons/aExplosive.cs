using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class aExplosive : MonoBehaviour
{
    [SerializeField]
    private float explosionRadius = 10f,
                  explosionForce = 1000f,
                  explosionDamage = 25f,
                  maxTorqueMultiplier = 50f,
                  fuseTime = 1.75f;

    // different grenades can throw differently?
    public virtual void SetThrowForce(float force)
    {
        GetComponent<Rigidbody>().AddForce(transform.right * force, ForceMode.Impulse);
    }

    private void Start()
    {
        ScoreManager.Instance.TrackPeripheralMetrics(MetricType.GrenadeThrown);
        Invoke("Explode", fuseTime);
    }

    private void Explode()
    {
        Instantiate(Resources.Load<GameObject>("Explosion"), transform.position, Quaternion.identity); // pls replace this effect

        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in objectsInRadius)
        {
            if (collider.attachedRigidbody)
            {
                collider.GetComponent<iHealth>()?.TakeDamage(explosionDamage, null);
                collider.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                Vector3 torque = collider.attachedRigidbody.velocity * Random.Range(maxTorqueMultiplier / 5, maxTorqueMultiplier);
                collider.attachedRigidbody.AddTorque(torque, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
