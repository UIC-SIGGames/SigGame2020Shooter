using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f,
                  damageAmt = 10f;
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, GameManager.DespawnTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<iHealth>()?.TakeDamage(damageAmt, collision);
        Destroy(gameObject);
    }
}