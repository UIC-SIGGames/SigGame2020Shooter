using UnityEngine;

public class BulletControl : MonoBehaviour, iBullet
{
    [SerializeField]
    private float speed = 20f;
    private float damageAmt = 0f;

    public void Initialize(float damage, Vector3 shooterVelocity)
    {
        if (!GameManager.VelocitySensitiveBullets)
            shooterVelocity = Vector3.zero;

        damageAmt = damage;

        GetComponent<Rigidbody>().velocity = transform.forward * speed + 
            new Vector3(transform.right.x * shooterVelocity.x, 0, transform.right.z * shooterVelocity.z);

        Destroy(gameObject, GameManager.DespawnTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<iHealth>()?.TakeDamage(damageAmt, collision);
        Destroy(gameObject);
    }
}