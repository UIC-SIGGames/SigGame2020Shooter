﻿using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
[RequireComponent(typeof(iWeapon))]
public class ShootImpulseHelper : MonoBehaviour
{
    private CinemachineImpulseSource impulse;
    private void Start()
    {
        // add if !player don't shake unless BFG?
        // in order to share guns between enemies and player

        impulse = GetComponent<CinemachineImpulseSource>();
        GetComponent<iWeapon>().OnFire += SendImpulse;
    }

    private void SendImpulse()
    {
        impulse.GenerateImpulse(Vector3.one * 0.5f);
    }
}
