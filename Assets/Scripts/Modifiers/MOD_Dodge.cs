using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOD_Dodge : MonoBehaviour
{
    [SerializeField]
    private KeyCode dodgeModifier = KeyCode.LeftShift;

    [SerializeField]
    private float dodgeForce = 50f,
                  dodgeRecovery = .5f,
                  batteryPenalty = 2.5f;

    private bool canDodge = true;
    private Rigidbody rb => GetComponent<Rigidbody>();

    private void LateUpdate()
    {
        if (canDodge && Input.GetKeyDown(dodgeModifier))
            Dodge();
    }

    private void Dodge()
    {
        canDodge = false;
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        BatteryManager.Instance?.RemoveEnergy(batteryPenalty);
        rb.velocity = direction.normalized * dodgeForce;
        Invoke("ResetDodge", dodgeRecovery);
    }

    private void ResetDodge() => canDodge = true;
}
