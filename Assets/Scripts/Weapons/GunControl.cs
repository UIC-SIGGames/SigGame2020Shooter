using System;
using System.Collections;
using UnityEngine;

public class GunControl : MonoBehaviour, iWeapon
{
    [SerializeField] private float bulletDamage = 10f,
                                   coolDownTime = 0.2f,
                                   energyConsumption = 1f,
                                   degreeInaccuracy = 3f;

    [SerializeField] private Transform firePoint = null;
    [SerializeField] private GameObject bulletPrefab = null;

    [Header("Gun Hitbox")]
    [SerializeField] private float shuntForce = 10f;

    private bool coolingDown = false;

    public event Action OnFire = delegate { };

    private Rigidbody parentRB => transform.parent.GetComponent<Rigidbody>();

    public float GetConsumption() { return energyConsumption; }

    public void CommandFire()
    {
        if (!coolingDown)
        {
            OnFire();
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        ScoreManager.Instance?.AddPoints(ScoreType.Shot);

        Vector3 rotation = transform.eulerAngles;
        rotation.y += UnityEngine.Random.Range(-degreeInaccuracy, degreeInaccuracy);

        coolingDown = true;
        ActivateHitbox();
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(rotation))
            .GetComponent<iBullet>().Initialize(bulletDamage, parentRB.velocity);
        yield return new WaitForSeconds(coolDownTime);

        coolingDown = false;
    }

    private void ActivateHitbox()
    {
        var enemies = Physics.OverlapBox(transform.position, HitboxDimensions, transform.rotation, LayerMask.GetMask("Hostile"));

        foreach (var enemy in enemies)
        {
            enemy.GetComponent<iHealth>().TakeDamage(bulletDamage, null);
            enemy.attachedRigidbody.AddForce((enemy.transform.position - transform.position) * shuntForce, ForceMode.Impulse);
        }
    }


    [SerializeField] Vector3 HitboxDimensions = new Vector3(0.2f, 0.2f, 0.2f);
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, HitboxDimensions);
    }
}