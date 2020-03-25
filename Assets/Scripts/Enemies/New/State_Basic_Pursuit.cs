using System;
using UnityEngine;

public class State_Basic_Pursuit : EnemyState
{
    public override Type Tick()
    {
        Vector3 nextPos =  Vector3.ClampMagnitude(enemy.Target.position - transform.position, 1);

        enemy.SetMoveProperties(nextPos, nextPos);

        float distance = Vector3.Distance(transform.position, enemy.Target.position);
        if (distance <= enemy.ChargeRange)
            return typeof(State_Basic_Charge);
        else if (distance >= enemy.LostRange) // add a timer
            return typeof(State_Basic_Seek);

        return null;
    }
}