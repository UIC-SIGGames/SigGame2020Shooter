using UnityEngine;

[RequireComponent(typeof(iHealth))]
public class sfxStopSignHit : MonoBehaviour
{
    [SerializeField]
    private AudioEvent audioEvent;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        GetComponent<iHealth>().OnTakeDamage += HitSound;
    }

    private void HitSound()
    {
        audioEvent.Play(audioSource);
    }
}
