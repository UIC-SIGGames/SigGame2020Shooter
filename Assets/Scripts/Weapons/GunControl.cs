﻿using System;
using System.Collections;
using UnityEngine;

public class GunControl : MonoBehaviour, iShoot
{
    [SerializeField] private float coolDownTime = 0.2f;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;

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
        BatteryManager.Instance?.RemoveEnergy(5f);

        coolingDown = true;
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(coolDownTime);
        coolingDown = false;
    }
}

