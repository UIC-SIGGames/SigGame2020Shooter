using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Health_Enemy : MonoBehaviour, iHealth
{
    [SerializeField] private float maxHealth = 5f;

    private float currentHealth;
    private HealthBar healthBar;
    public event Action OnTakeDamage = delegate { };
    public event Action<EnemyStates> OnChangeState = delegate { };

    private void Start()
    {
        InitializeHealthBar();

        currentHealth = maxHealth;
        healthBar.SetFill(PercentLeft());
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.ChangeFill(PercentLeft());

        ScoreManager.Instance?.AddPoints(ScoreType.Hit); // probably this too
        BatteryManager.Instance?.AddEnergy(10f);

        if (currentHealth <= 0)
            OnChangeState(EnemyStates.Dead);

        OnTakeDamage();
        OnChangeState(EnemyStates.Stunned);
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
        healthBar = (newHealthBarObject.GetComponent<HealthBar>());
    }
}

public enum EnemyStates
{
    Patrolling,
    Seeking,
    MovingTowardsTarget,
    Attacking,
    Stunned,
    Dead
}