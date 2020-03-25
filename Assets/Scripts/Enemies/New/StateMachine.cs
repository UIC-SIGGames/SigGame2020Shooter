using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public void SetStates(Dictionary<Type, EnemyState> states)
    {
        availableStates = states;
        ActiveState = availableStates.Values.FirstOrDefault();
    }

    public EnemyState ActiveState { get; private set; }
    public Dictionary<Type, EnemyState> availableStates;

    private void Update()
    {
        var nextState = ActiveState.Tick();

        if (nextState != null)
            ActiveState = availableStates[nextState];
    }
}