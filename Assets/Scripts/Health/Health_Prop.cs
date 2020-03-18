using System;
using System.Collections;
using UnityEngine;

public class Health_Prop : MonoBehaviour, iHealth
{
    public event Action OnTakeDamage = delegate { };

    public float PercentLeft()
    {
        return 100f;
    }

    public void TakeDamage(float amount)
    {
        ScoreManager.Instance?.AddPoints(ScoreType.Destructible);
        OnTakeDamage();
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(GameManager.DespawnTime);
        Destroy(gameObject);
    }
}