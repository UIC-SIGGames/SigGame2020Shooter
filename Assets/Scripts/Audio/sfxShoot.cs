using UnityEngine;

[RequireComponent(typeof(GunControl))]
public class sfxShoot : MonoBehaviour
{
    [SerializeField]
    private AudioEvent audioEvent;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        GetComponent<GunControl>().OnFire += ShootSound;
    }

    private void ShootSound()
    {
        audioEvent.Play(audioSource);
    }
}
