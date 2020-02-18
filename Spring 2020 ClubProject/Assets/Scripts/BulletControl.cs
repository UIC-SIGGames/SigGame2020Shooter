using System.Collections;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] private float speed = 50f,
                                   despawnTime = 2f;

    IEnumerator despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(despawn());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime );
    }
}
