using UnityEngine;

public class MOD_DrainOverTime : MonoBehaviour, iModifier
{
    [SerializeField] private float depletionRate = 2f;
    private void Update()
    {
        BatteryManager.Instance?.RemoveEnergy(depletionRate * Time.deltaTime);
    }
}
