using System;
using UnityEngine;
public interface iHealth
{
    void TakeDamage(float amount, Vector3 normal);
    float PercentLeft();

    event Action OnTakeDamage;
}
