using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Basic Properties")]
    public LayerMask attackLayer;
    public float MoveSpeed = 10f,
                 TurnSpeed = 2f;
    [Header("Seek Behavior")]
    public float TimeBtwnDecisions = 8f,
                 EyeSight = 10f,
                 WallCheckDistance = 4.5f;
    [Header("Pursuit Behavior")]
    public float ChargeRange = 4f;
    public float LostRange = 100f;

    public Transform Target { get; private set; }
    public Rigidbody Rb { get; private set; }

    private StateMachine stateMachine;
    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        nextPosition = transform.position;

        InitializeStates();
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    private Vector3 nextPosition;
    private Quaternion nextRotation;
    public void SetMoveProperties(Vector3 posOffset, Vector3 rotationOffset, float moveSpeedMultiplier = 1f)
    {
        nextPosition = transform.position + posOffset * MoveSpeed * Time.fixedDeltaTime * moveSpeedMultiplier;
        nextRotation = Quaternion.Lerp(
            transform.rotation, 
            Quaternion.LookRotation(rotationOffset), 
            Time.fixedDeltaTime * TurnSpeed);
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Rb.MovePosition(nextPosition);
        transform.rotation = nextRotation;
    }

    private void InitializeStates()
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
}