﻿using UnityEngine;
using System;

public class Health_Enemy : MonoBehaviour, iHealth
{
    [SerializeField] private float maxHealth = 5f;

    private float currentHealth;
    private HealthBar healthBar;
    public event Action OnTakeDamage = delegate { };

    private void Start()
    {
        InitializeHealthBar();

        currentHealth = maxHealth;
        healthBar.SetFill(PercentLeft());
    }

    public void TakeDamage(float amount, Collision collision = null)
    {
        currentHealth -= amount;
        healthBar.ChangeFill(PercentLeft());

        ScoreManager.Instance?.AddPoints((collision != null) ? ScoreType.Hit : ScoreType.HitMartyrdom);

        if(collision != null)
            Instantiate(Resources.Load<GameObject>("Shot Impact"), 
                collision.contacts[0].point, 
                Quaternion.FromToRotation(Vector3.forward, collision.contacts[0].normal), 
                transform);

        OnTakeDamage();
    }

    public float PercentLeft()
    {
        return currentHealth / maxHealth;
    }

    private void InitializeHealthBar()
    {
        var newHealthBarObject = Instantiate(Resources.Load<GameObject>("Health Bar"));
        newHealthBarObject.name = gameObject.name + "'s health bar";
        newHealthBarObject.GetComponent<Hover>().SetTarget(gameObject.transform, Vector3.up * 2);
        healthBar = newHealthBarObject.GetComponent<HealthBar>();
    }
}