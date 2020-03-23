using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOD_Invincible : MonoBehaviour, iModifier
{
    private void Start()
    {
        GetComponent<MOD_TakesDamage>().enabled = false;
    }

    private void OnDisable()
    {
        GetComponent<MOD_TakesDamage>().enabled = true;
        Destroy(this);
    }

    private void HandleImpacts(Collision collision)
    {
        // could bounce bullets back?
        // could do pingy sounds?
        // possibilities are endless
    }

    private void OnCollisionEnter(Collision collision) { HandleImpacts(collision); }
    private void OnCollisionStay(Collision collision) { HandleImpacts(collision); }
}