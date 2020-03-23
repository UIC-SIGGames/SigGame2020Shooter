using System;
using System.Collections;
using UnityEngine;

public class GunControl : MonoBehaviour, iWeapon
{
    [SerializeField] private float coolDownTime = 0.2f;
    [SerializeField] private float energyConsumption = 1f;
    [SerializeField] private float degreeInaccuracy = 3f;

    [SerializeField] private Transform firePoint = null;
    [SerializeField] private GameObject bullet = null;

    private bool coolingDown = false;

    public event Action OnFire = delegate { };

    public float GetConsumption() { return energyConsumption; }

    public void CommandFire()
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

        Vector3 rotation = transform.eulerAngles;
        rotation.y += UnityEngine.Random.Range(-degreeInaccuracy, degreeInaccuracy);

        coolingDown = true;
        Instantiate(bullet, firePoint.position, Quaternion.Euler(rotation));

        yield return new WaitForSeconds(coolDownTime);

        coolingDown = false;
    }
}