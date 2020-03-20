using UnityEngine;

[RequireComponent(typeof(iPickup))]
public class sfxPickup : MonoBehaviour
{
    [SerializeField]
    private AudioEvent audioEvent = null;

    private AudioSource audioSource = null;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        GetComponent<iPickup>().OnPickup += PickupSound;
    }

    private void PickupSound()
    {
        audioEvent.Play(audioSource);
    }
}
