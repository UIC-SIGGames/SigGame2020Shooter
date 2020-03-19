using UnityEngine;

public class Hover : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;

    internal void SetTarget(Transform newTarget, Vector3 newOffset)
    {
        target = newTarget;
        offset = newOffset;
    }

    private void LateUpdate()
    {
        if (target == null)
            Destroy(gameObject); // recycle instead
        else
            transform.position = target.position + offset;
    }
}
