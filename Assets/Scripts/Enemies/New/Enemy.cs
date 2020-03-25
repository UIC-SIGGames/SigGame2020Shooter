using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : aEnemy
{
    #region Customizables
    [Header("Seek Behavior")]
    public float TimeBtwnDecisions = 8f;
    public float EyeSight = 10f,
                 WallCheckDistance = 4.5f;

    [Header("Pursuit Behavior")]
    public float ChargeRange = 8f;
    public float LostRange = 40f;

    [Header("Charge Behavior")]
    public float ObserveTime = .25f;
    public float ChargeTime = 1.25f,
                 RecoverTime = .25f,
                 PredictionScale = 2f;
    #endregion

    #region State Machine Stuff
    protected override void InitializeStates()
    {
        var states = new Dictionary<Type, EnemyState>()
        {
            {  typeof(State_Basic_Seek), gameObject.AddComponent<State_Basic_Seek>() },
            {  typeof(State_Basic_Stunned), gameObject.AddComponent<State_Basic_Stunned>() },
            {  typeof(State_Basic_Pursuit), gameObject.AddComponent<State_Basic_Pursuit>() },
            {  typeof(State_Basic_Charge), gameObject.AddComponent<State_Basic_Charge>() },
        };

        stateMachine = gameObject.AddComponent<StateMachine>();
        stateMachine.SetStates(states);
    }
    #endregion
}