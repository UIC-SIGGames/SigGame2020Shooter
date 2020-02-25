using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, iHealth
{
    [SerializeField] private float maxHealth = 5f,
                                   despawnTime = 4.0f;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject healthCanvas;

    private float currentHealth;

    public event Action OnTakeDamage = delegate { };

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = PercentLeft();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();

        OnTakeDamage();
    }

    public float PercentLeft()
    {
        return currentHealth / maxHealth;
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = PercentLeft();

        if (currentHealth <= 0)
        {
            healthCanvas.SetActive(false);
            StartCoroutine(Despawn());
        }
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}