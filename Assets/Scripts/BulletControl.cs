using System.Collections;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] private float speed = 50f,
                                   despawnTime = 2f;
                                   AudioSource sound;
                                   [SerializeField]
                                   private float min;
                                   [SerializeField]
                                   private float max;

    IEnumerator despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.pitch = Random.Range(min, max);
        StartCoroutine(despawn());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime );
    }
}
