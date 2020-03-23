using System;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    [SerializeField]
    private float startingEnergy = 100;

    private float energyLeft;

    public static BatteryManager Instance;
    public static event Action<float> OnEnergyChange = delegate { };

    private void Start()
    {
        Instance = this;
        energyLeft = startingEnergy;
    }

    private void ChangeEnergy(float amount)
    {
        energyLeft += amount;
        OnEnergyChange(energyLeft / startingEnergy);
    }

    public void AddEnergy(float amount)
    {
        ChangeEnergy(amount);
    }

    public void RemoveEnergy(float amount)
    {
        ChangeEnergy(-amount);
    }

    public bool LowEnergy()
    {
        return energyLeft < startingEnergy * 0.3f;
    }
}