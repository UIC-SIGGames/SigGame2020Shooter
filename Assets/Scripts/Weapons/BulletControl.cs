using System.Collections;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f,
                  damageAmt = 10f;
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(GameManager.DespawnTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        collision.gameObject.GetComponent<iHealth>()?.TakeDamage(damageAmt, collision);
        Destroy(gameObject);
    }
}
