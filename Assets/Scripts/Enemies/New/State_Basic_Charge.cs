using System;
using System.Collections;
using UnityEngine;

public class State_Basic_Charge : EnemyState
{
    private float chargeTimer;

    private Vector3 targetDirection;
    private ChargeState state = ChargeState.Start;

    // Observe delta in player movement to predict direction
    // Charge past that direction
    private IEnumerator Observe()
    {
        Vector3 beginPos = enemy.Target.position;
        yield return new WaitForSeconds(enemy.ObserveTime);
        Vector3 endPos = enemy.Target.position;

        Vector3 targetPos = endPos + (endPos - beginPos);
        targetDirection = targetPos - transform.position;
        targetDirection = Vector3.ClampMagnitude(targetDirection, 1);

        enemy.transform.LookAt(transform.position + targetDirection);
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
        Debug.Log("Charging");
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
                return typeof(State_Basic_Pursuit);
        }

        return null;
    }

    // do collision detection and shunt
    private void Charge()
    {
        enemy.SetMoveProperties(targetDirection, targetDirection, 2);
        chargeTimer -= Time.deltaTime;
    }

    public override void Interrupt()
    {
        throw new NotImplementedException();
    }

    private enum ChargeState
    {
        Start,
        Idle, // for Observing and recovering
        Charge,
        Finished
    }
}