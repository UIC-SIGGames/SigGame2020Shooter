using System.Collections;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField] private float coolDownTime = 0.2f;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;

    private bool coolingDown = false;

    internal void CommandFire()
    {
        if(!coolingDown)    
            StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        coolingDown = true;
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(coolDownTime);
        coolingDown = false;
    }
}

