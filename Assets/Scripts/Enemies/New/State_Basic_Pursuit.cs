using System;
using UnityEngine;

public class State_Basic_Pursuit : EnemyState
{
    private float chargeRange = 4f;

    public override Type Tick()
    {
        Debug.Log("Pursuing");

        enemy.Rb.MovePosition(Vector3.Lerp(transform.position, enemy.Target.position, enemy.MoveSpeed * Time.deltaTime));
        transform.LookAt(enemy.Target);

        if (Vector3.Distance(transform.position, enemy.Target.position) <= chargeRange)
            return typeof(State_Basic_Charge);

        return null;
    }
}