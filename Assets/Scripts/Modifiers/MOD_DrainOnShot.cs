using UnityEngine;

public class MOD_DrainOnShot : MonoBehaviour, iModifier
{
    private float energyConsumption;
    private iWeapon weapon;

    private void FindWeapon()
    {
        weapon = GetComponentInChildren<iWeapon>();
        weapon.OnFire += Tick;
        energyConsumption = weapon.GetConsumption();
    }

    private void Tick()
    {
        BatteryManager.Instance?.RemoveEnergy(energyConsumption);
    }

    private void LateUpdate()
    {
        if (weapon == null)
            FindWeapon();
    }

    private void OnEnable()
    {
        FindWeapon();
    }

    private void OnDisable()
    {
        if (weapon != null)
            weapon.OnFire -= Tick;
    }
}
