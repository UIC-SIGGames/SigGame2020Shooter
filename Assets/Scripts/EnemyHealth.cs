using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, iHealth
{
    [SerializeField] private float maxHealth = 5f,
                                   despawnTime = 4.0f;

    private HealthBar healthBar;

    private float currentHealth;

    public event Action OnTakeDamage = delegate { };

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetFill(PercentLeft());
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.ChangeFill(PercentLeft());

        if (currentHealth <= 0)
            StartCoroutine(Despawn());

        OnTakeDamage();
    }

    public float PercentLeft()
    {
        return currentHealth / maxHealth;
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}