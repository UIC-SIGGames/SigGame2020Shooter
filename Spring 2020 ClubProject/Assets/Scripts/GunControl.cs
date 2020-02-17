using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GunControl : MonoBehaviour
{

    public bool isFiring;
    bool canFire = true; 

    public BulletControl bullet; 
    public float bulletSpeed; 
    public float timeBetweenShots; 
    private float shotCounter; 
    public Transform firePoint; 

    IEnumerator coolDown()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canFire = true;
        yield return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( isFiring){
            canFire = false;
            
                shotCounter = timeBetweenShots; 
                BulletControl newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletControl;
                newBullet.speed = bulletSpeed;
                StartCoroutine(coolDown());
        }
            else {
                shotCounter = 0; 
            }
        }
    }

