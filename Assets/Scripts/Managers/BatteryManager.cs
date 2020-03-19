﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    [SerializeField]
    private float startingEnergy = 100,
                  depletionRate  = 5f;

    private float energyLeft;

    public static BatteryManager Instance;
    public static event Action<float> OnEnergyChange = delegate { };

    private void Start()
    {
        Instance = this;
        energyLeft = startingEnergy;
    }

    private void Update()
    {
        if (energyLeft <= 0)
            ; // die
    }

    public void AddEnergy(float amount)
    {
        energyLeft += amount;
    }

    public void RemoveEnergy(float amount)
    {
        energyLeft -= amount;
        OnEnergyChange(energyLeft / startingEnergy);
    }

    public bool LowEnergy()
    {
        return energyLeft < startingEnergy * 0.3f;
    }

    private void DepleteEnergy() // use this if we want an artificial time limit
    {
        energyLeft -= depletionRate * Time.deltaTime;
        OnEnergyChange(energyLeft / startingEnergy);
    }
}
