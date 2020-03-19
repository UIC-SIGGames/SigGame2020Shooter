using UnityEngine;

[RequireComponent(typeof(iHealth))]
public class sfxTakeDamage : MonoBehaviour
{
    [SerializeField]
    private AudioEvent audioEvent = null;

    private AudioSource audioSource = null;
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
