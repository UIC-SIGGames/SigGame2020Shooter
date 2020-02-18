using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GunControl weapon;

    private bool hasWeapon = false;

    private void Start()
    {
        weapon = GetComponentInChildren<GunControl>();

        if (weapon != null)
            hasWeapon = true;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && hasWeapon)
            weapon.CommandFire();
    }
}
