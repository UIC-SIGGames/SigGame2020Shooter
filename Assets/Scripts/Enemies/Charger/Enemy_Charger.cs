using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Charger : aEnemy
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
            {  typeof(State_Charger_Seek), gameObject.AddComponent<State_Charger_Seek>() },
            {  typeof(State_Charger_Stunned), gameObject.AddComponent<State_Charger_Stunned>() },
            {  typeof(State_Charger_Pursuit), gameObject.AddComponent<State_Charger_Pursuit>() },
            {  typeof(State_Charger_Charge), gameObject.AddComponent<State_Charger_Charge>() },
            {  typeof(State_Charger_Dead), gameObject.AddComponent<State_Charger_Dead>() }
        };

        stateMachine = gameObject.AddComponent<StateMachine>();
        stateMachine.SetAvailableStates(states);
    }
    #endregion
}