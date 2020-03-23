using Cinemachine;
using System.Collections;
using UnityEngine;

public class MOD_TakesDamage : MonoBehaviour, iModifier
{
    [Header("Damaged Properties")]
    [SerializeField] private float damageAmount = 30f;
    [SerializeField] private float recoveryTime = 0.8f,
                                   screenShakeAmount = 1.2f,
                                   shuntForce = 10f;

    private bool takingHits = true;

    private CinemachineImpulseSource impulseSource;
    private Rigidbody rb;

    private void OnEnable()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        rb = GetComponent<Rigidbody>();

        var godMod = GetComponent<MOD_Invincible>();
        if (godMod != null)
            Destroy(godMod);
    }

    private void OnDisable()
    {
        if (GetComponent<Player>() != null)
            gameObject.AddComponent<MOD_Invincible>();
    }

    private void HandleCollision(Collision collision)
    {
        if (enabled)
        {
            var enemy = collision.gameObject.GetComponent<iEnemy>();

            if (enemy != null && !enemy.IsDead() && takingHits)
            {
                StartCoroutine(TakeHit(collision.contacts[0].normal));
            }
        }
    }

    private void Shunt(Vector3 normal)
    {
        rb.velocity += (normal * shuntForce);
    }

    private IEnumerator TakeHit(Vector3 normal)
    {
        impulseSource.GenerateImpulse(Vector3.one * screenShakeAmount);
        Shunt(normal);
        takingHits = false;
        // add a blinking effect on the player model
        BatteryManager.Instance?.RemoveEnergy(damageAmount);
        yield return new WaitForSeconds(recoveryTime);
        takingHits = true;
    }

    private void OnCollisionEnter(Collision collision) { HandleCollision(collision); }
    private void OnCollisionStay(Collision collision) { HandleCollision(collision); }
}