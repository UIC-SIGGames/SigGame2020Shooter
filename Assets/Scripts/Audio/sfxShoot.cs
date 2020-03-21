using UnityEngine;

[RequireComponent(typeof(iWeapon))]
public class sfxShoot : MonoBehaviour
{
    [SerializeField]
    private AudioEvent audioEvent = null;

    private AudioSource audioSource = null;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        GetComponent<iWeapon>().OnFire += ShootSound;
    }

    private void ShootSound()
    {
        audioEvent.Play(audioSource);
    }
}
