﻿using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnEnd = delegate { };
    public static float DespawnTime { get; private set; }
    public static float BatPickupBonus { get; private set; }
    public static bool GameOver { get; private set; }

    [SerializeField] private float despawnTime = 4.0f;
    [SerializeField] private float batteryPickupEnergyBonus = 10f;
    [SerializeField] private float blowUpShakeAmount = 10f;

    private void Start()
    {
        DespawnTime = despawnTime;
        BatPickupBonus = batteryPickupEnergyBonus;
        GameOver = false;
        BatteryManager.OnEnergyChange += EndGame;
    }

    private void EndGame(float healthPercent)
    {
        if (healthPercent >= 0)
            return;
        if(!GameOver)
        {
            GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulse(Vector3.one * blowUpShakeAmount);
            GameOver = true;
            Debug.Log("Game over");
            OnEnd();
        }
    }
}