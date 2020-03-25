using System;
using UnityEngine;

public class State_Basic_Stunned : EnemyState
{
    private float stunTime = 0.5f;

    private bool stunned;
    private float timer;

    public override void Interrupt()
    {
        throw new NotImplementedException();
    }

    public override Type Tick()
    {
        Debug.Log("Stunned");

        if (!stunned)
        {
            stunned = true;
            timer = UnityEngine.Random.Range(.75f, 1.25f) * stunTime;
        }
        else if (timer <= 0)
        {
            stunned = false;
            return typeof(State_Basic_Pursuit);
        }

        timer -= Time.deltaTime;
        return null;
    }
}