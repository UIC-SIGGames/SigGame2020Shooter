using System;
using System.Collections;
using UnityEngine;

public class State_Charger_Dead : EnemyState
{
    private  Enemy_Charger enemy;
    private void Start()
    {
        enemy = GetComponent<Enemy_Charger>();
    }

    public override Type Interrupt(InterruptTypes interrupt)
    {
        return null;
    }

    public override Type Tick()
    {
        if(!enemy.IsDead)
        {
            StartCoroutine(Die());
        }

        return null;
    }

    private IEnumerator Die()
    {
        enemy.SetDead();
        SpawnBattery(UnityEngine.Random.Range(0, enemy.MaxNumBatteriesDropped));
        ScoreManager.Instance?.AddPoints(ScoreType.BasicEnemy); // change probably
        yield return new WaitForSeconds(GameManager.DespawnTime);
        Destroy(gameObject);
    }

    private void SpawnBattery(int numBatteries)
    {
        bool? playerLowHealth = BatteryManager.Instance?.LowEnergy();

        if (playerLowHealth.Value && numBatteries < 2)
            numBatteries = 2;

        while (numBatteries > 0)
            Instantiate(Resources.Load<GameObject>("Battery"), transform.position + Vector3.up * numBatteries--, Quaternion.identity);
    }
}