using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float coolDownTime = 4.0f;
    public float health; 
    public float maxHealth; 

    public GameObject healthBar; 
    public Slider mainSlider;

    public GameObject fill; 

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        mainSlider.value = CalculateHealth(); 
    }

    // Update is called once per frame
    void Update()
    {
        mainSlider.value = CalculateHealth(); 
        if(health < maxHealth){
            healthBar.SetActive(true); 
        }
         if(health <= 0){
            fill.SetActive(false); 
            StartCoroutine(die());
             
         }

         if(health >maxHealth){
             health = maxHealth; 
         }
    }


 void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet_1" ){
            health = health - 100; 
            CalculateHealth(); 
        }
    }


float  CalculateHealth(){
        return health / maxHealth; 
    }


IEnumerator die()
    {
       
        yield return new WaitForSeconds(coolDownTime);
     Destroy(this.gameObject); 
    }


}

