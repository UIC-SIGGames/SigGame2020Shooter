using System;
using System.Linq;
using UnityEngine;

public class State_Stunned : EnemyState
{
    private float targetScanRadius = 10f;

    private bool stunned = false;
    private float timer;

    private aEnemy enemy;

    private Type pursuit, seek;

    private void Start()
    {
        enemy = GetComponent<aEnemy>();
    }

    public void SetTypes(Type pursuit, Type seek)
    {
        this.pursuit = pursuit;
        this.seek = seek;
    }

    public override Type Interrupt(InterruptTypes interrupt)
    {
        if (interrupt == InterruptTypes.Hit)
            timer += enemy.StunTime;
        else
            return typeof(State_Dead);

        return null;
    }

    public override Type Tick()
    {
        if (!stunned)
        {
            stunned = true;
            timer = UnityEngine.Random.Range(.75f, 1.25f) * enemy.StunTime;
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

                return pursuit;
            }

            return seek;
        }

        timer -= Time.deltaTime;
        return null;
    }
}