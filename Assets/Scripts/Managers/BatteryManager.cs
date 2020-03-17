using System;
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

        if (!GameManager.gameOver)
            DepleteEnergy();
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

    private void DepleteEnergy()
    {
        energyLeft -= depletionRate * Time.deltaTime;
        OnEnergyChange(energyLeft / startingEnergy);
    }
}
