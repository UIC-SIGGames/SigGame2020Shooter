using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float despawnTime;

    IEnumerator despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(this.gameObject);
        yield return null;
    }
    public float speed; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(despawn());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime );
    }
}
