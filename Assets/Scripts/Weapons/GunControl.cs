using System;
using System.Collections;
using UnityEngine;

public class GunControl : MonoBehaviour, iShoot
{
    [SerializeField] private float coolDownTime = 0.2f;
    [SerializeField] private float energyConsumption = 1f;

    [SerializeField] private Transform firePoint = null;
    [SerializeField] private GameObject bullet = null;

    private bool coolingDown = false;

    public event Action OnFire = delegate { };

    internal void CommandFire()
    {
        if (!coolingDown)
        {
            OnFire();
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        ScoreManager.Instance?.AddPoints(ScoreType.Shot);
        BatteryManager.Instance?.RemoveEnergy(energyConsumption);

        coolingDown = true;
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(coolDownTime);
        coolingDown = false;
    }
}

