using UnityEngine;

public class MOD_DrainOnMove : MonoBehaviour, iModifier
{
    [SerializeField] private float energyLossRate = 0.045f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude > 1)
            BatteryManager.Instance?.RemoveEnergy(energyLossRate);
    }
}
