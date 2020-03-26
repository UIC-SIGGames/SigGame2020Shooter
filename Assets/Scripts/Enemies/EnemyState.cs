using System;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public abstract Type Tick();
    public virtual Type Interrupt(InterruptTypes interrupt)
    {
        if (interrupt == InterruptTypes.Hit)
            return typeof(State_Stunned);
        else
            return typeof(State_Dead);
    }
}