using System;
using UnityEngine;

public class State_Charger_Pursuit : EnemyState
{
    private float distanceToTarget;

    private Enemy_Charger enemy => GetComponent<Enemy_Charger>();

    public override Type Tick()
    {
        Vector3 nextPos =  Vector3.ClampMagnitude(enemy.Target.position - transform.position, 1);

        enemy.SetMoveProperties(nextPos, nextPos);

        distanceToTarget = Vector3.Distance(transform.position, enemy.Target.position);
        if (distanceToTarget <= enemy.ChargeRange)
            return typeof(State_Charger_Charge);
        else if (LostTarget())
            return typeof(State_Charger_Seek);

        return null;
    }

    private bool LostTarget() 
    {
        // add a "boredom" timer
        // add raycast to check if obstructions lie between enemy and target
        return distanceToTarget >= enemy.LostRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemy.ChargeRange);
    }
}