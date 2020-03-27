using System;
using System.Collections;
using UnityEngine;

public class State_Charger_Charge : EnemyState
{
    private float chargeTimer;

    private Vector3 targetDirection;
    private ChargeState state = ChargeState.Start;

    private Enemy_Charger enemy => GetComponent<Enemy_Charger>();

    // Observe delta in player movement to predict direction
    // Charge past that direction
    private IEnumerator Observe()
    {
        Vector3 beginPos = enemy.Target.position;
        yield return new WaitForSeconds(enemy.ObserveTime);
        Vector3 endPos = enemy.Target.position;

        Vector3 targetPos = endPos + (endPos - beginPos) * enemy.PredictionScale;
        targetDirection = targetPos - transform.position;
        targetDirection = Vector3.ClampMagnitude(targetDirection, 1);

        transform.LookAt(transform.position + targetDirection);
        chargeTimer = enemy.ChargeTime;
        state = ChargeState.Charge;
    }

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(enemy.RecoverTime);
        state = ChargeState.Finished;
    }

    public override Type Tick()
    {
        switch (state)
        {
            case ChargeState.Start:
                StartCoroutine(Observe());
                state = ChargeState.Idle;
                break;

            case ChargeState.Charge:
                Charge();
                if (chargeTimer <= 0)
                {
                    StartCoroutine(Recover());
                    state = ChargeState.Idle;
                }
                break;

            case ChargeState.Finished:
                state = ChargeState.Start;
                return typeof(State_Charger_Pursuit);
        }

        return null;
    }

    // do collision detection and shunt
    private void Charge()
    {
        enemy.SetMoveProperties(targetDirection, targetDirection, 2);
        chargeTimer -= Time.deltaTime;
    }

    public override Type Interrupt(InterruptTypes interrupt)
    {
        if (interrupt == InterruptTypes.Dead)
            return typeof(State_Dead);

        return null;
    }

    private enum ChargeState
    {
        Start,
        Idle, // for Observing and recovering
        Charge,
        Finished
    }
}