using UnityEngine;

public class DeathExplosion : MonoBehaviour
{
    [SerializeField]
    private float maxScale = 20f,
                  speed = 10f,
                  fadeSpeed = 1f;

    private Vector3 targetScale;
    private Material material;

    private void Start()
    {
        targetScale = transform.localScale * maxScale;
        material = GetComponent<Renderer>().material;
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        if (transform.localScale.x <= targetScale.x - 0.5f)
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
        else
            material.SetFloat("Vector1_1A0AF951", material.GetFloat("Vector1_1A0AF951") - (fadeSpeed * Time.deltaTime));
    }
}
