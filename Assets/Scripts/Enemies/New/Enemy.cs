using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Target { get; private set; }
    public Rigidbody Rb { get; private set; }
    public float MoveSpeed { get; private set; }

    public float Eyesight { get; private set; }
    public float TurnSpeed { get; private set; }

    public LayerMask attackLayer;

    private StateMachine stateMachine;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Eyesight = 10f;
        MoveSpeed = 10f;
        TurnSpeed = 2f;

        InitializeStates();
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void Move(Vector3 posOffset, Vector3 rotationOffset, float moveSpeedMultiplier = 1f)
    {
        pOff = posOffset;
        rotOff = rotationOffset;
    }
    private Vector3 pOff, rotOff;
    private void FixedUpdate()
    {
        Rb.MovePosition(transform.position + pOff * 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rotOff), Time.deltaTime * TurnSpeed);

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