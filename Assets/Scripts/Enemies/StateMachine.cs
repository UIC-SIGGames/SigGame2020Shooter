using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public EnemyState ActiveState { get; private set; }
    public Dictionary<Type, EnemyState> availableStates;

    private void Update()
    {
        var nextState = ActiveState?.Tick();

        SetState(nextState);
    }

    public void SetAvailableStates(Dictionary<Type, EnemyState> states)
    {
        availableStates = states;
        ActiveState = availableStates.Values.FirstOrDefault();
    }

    private void SetState(Type state)
    {
        if (state != null)
            ActiveState = availableStates[state];
    }

    public void SendInterrupt(InterruptTypes interruptType)
    {
        var nextState = ActiveState.Interrupt(interruptType);

        SetState(nextState);
    }
}

public enum InterruptTypes
{
    Hit,
    Dead
}