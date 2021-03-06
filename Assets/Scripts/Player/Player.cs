﻿using UnityEngine;
using System;
using System.Collections;

[SelectionBase]
public class Player : MonoBehaviour
{
    [Header("Weapon Properties")]
    [SerializeField] private float grenadeThrowForce = 10f; // hold to throw farther?
    [SerializeField] private float grenadeWaitTime = 5f;
    [SerializeField] private GameObject explosiveWeapon;

    [Header("Death Properties")]
    [SerializeField] private float explosionRadius = 20f;
    [SerializeField]
    private float explosiveForce = 1000f,
                  maxTorqueMultiplier = 50f;

    private Rigidbody rb;
    private iWeapon weapon; // maybe have max 2 weapons - l/r click? round robin replace?
    private bool canThrowGrenade = true; // put all this grenade stuff in its own script

    private void Start()
    {
        weapon = GetComponentInChildren<iWeapon>();
        rb = GetComponent<Rigidbody>();
        GameManager.OnEnd += Die;
    }

    private void OnDestroy()
    {
        GameManager.OnEnd -= Die;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            weapon?.CommandFire();
        if (Input.GetMouseButton(1) && canThrowGrenade)
            StartCoroutine(ThrowGrenade());
    }

    private IEnumerator ThrowGrenade()
    {
        canThrowGrenade = false;
        Instantiate(explosiveWeapon, transform.position + transform.right * 2 + Vector3.up * 3, transform.rotation)
            .GetComponent<aExplosive>().SetThrowForce(grenadeThrowForce);
        yield return new WaitForSeconds(grenadeWaitTime);
        canThrowGrenade = true;
    }

    private void Die()
    {
        Destroy(GetComponent<PlayerModelTweening>());
        Destroy(GetComponent<PlayerMovement>());

        BlowUp();

        Destroy(this);
    }

    private void BlowUp()
    {
        Instantiate(Resources.Load<GameObject>("Explosion"), transform.position, Quaternion.identity); // pls replace this effect

        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;

        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in objectsInRadius)
        {
            if (collider.attachedRigidbody)
            {
                collider.GetComponent<iHealth>()?.TakeDamage(1000, null);
                collider.attachedRigidbody.AddExplosionForce(explosiveForce, transform.position, explosionRadius);
                Vector3 torque = collider.attachedRigidbody.velocity * UnityEngine.Random.Range(maxTorqueMultiplier / 5, maxTorqueMultiplier);
                collider.attachedRigidbody.AddTorque(torque, ForceMode.Impulse);
            }
        }
    }
}