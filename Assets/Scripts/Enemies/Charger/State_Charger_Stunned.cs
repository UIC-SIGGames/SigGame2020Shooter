using System;
using System.Linq;
using UnityEngine;

public class State_Charger_Stunned : EnemyState
{
    private float stunTime = 0.5f;
    private float targetScanRadius = 10f;

    private bool stunned = false;
    private float timer;

    private Enemy_Charger enemy;
    private void Start()
    {
        enemy = GetComponent<Enemy_Charger>();
    }

    public override Type Interrupt(InterruptTypes interrupt)
    {
        if (interrupt == InterruptTypes.Hit)
            timer += stunTime;
        else
            return typeof(State_Charger_Dead);

        return null;
    }

    public override Type Tick()
    {
        if (!stunned)
        {
            stunned = true;
            timer = UnityEngine.Random.Range(.75f, 1.25f) * stunTime;
        }
        else if (timer <= 0)
        {
            stunned = false;
            var potentialTargets = Physics.OverlapSphere(transform.position, targetScanRadius, enemy.attackLayer); // in case of MP -> attack nearest player
            if (potentialTargets.Length > 0)
            {
                Transform target = potentialTargets
                    .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
                    .FirstOrDefault().transform;

                enemy.SetTarget(target);

                return typeof(State_Charger_Pursuit);
            }

            return typeof(State_Charger_Seek);
        }

        timer -= Time.deltaTime;
        return null;
    }
}