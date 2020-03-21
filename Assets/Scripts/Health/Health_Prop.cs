using System;
using UnityEngine;

public class Health_Prop : MonoBehaviour, iHealth
{
    public event Action OnTakeDamage = delegate { };

    public float PercentLeft()
    {
        return 100f;
    }

    public void TakeDamage(float amount, Collision collision = null)
    {
        ScoreManager.Instance?.AddPoints(ScoreType.Destructible);
        OnTakeDamage();
        Destroy(gameObject, GameManager.DespawnTime);
    }
}