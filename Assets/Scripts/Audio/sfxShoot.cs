using UnityEngine;

[RequireComponent(typeof(iShoot))]
public class sfxShoot : MonoBehaviour
{
    [SerializeField]
    private AudioEvent audioEvent = null;

    private AudioSource audioSource = null;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        GetComponent<iShoot>().OnFire += ShootSound;
    }

    private void ShootSound()
    {
        audioEvent.Play(audioSource);
    }
}
