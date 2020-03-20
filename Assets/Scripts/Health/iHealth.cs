using System;
using UnityEngine;
public interface iHealth
{
    void TakeDamage(float amount, Collision collision);
    float PercentLeft();

    event Action OnTakeDamage;
}
