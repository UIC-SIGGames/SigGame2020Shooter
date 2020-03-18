using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// currently throws a null exception after its target dies, doesn't affect anything
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

        transform.position = target.position + offset;
    }
}
