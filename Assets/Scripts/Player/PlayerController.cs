using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GunControl weapon;

    private void Start()
    {
        weapon = GetComponentInChildren<GunControl>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            weapon?.CommandFire();
    }
}
