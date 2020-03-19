using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Event")]
public class AudioEvent : ScriptableObject
{
    [SerializeField]
    private AudioClip[] audioClips = null;
    [SerializeField]
    private Vector2 volumeRange = new Vector2(0.5f, 1.0f),
                    pitchRange = new Vector2(0.75f, 1.25f);

    private int clipIndex = 0;
    public void Play(AudioSource audioSource)
    {
        clipIndex = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[clipIndex];

        audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
        audioSource.volume = Random.Range(volumeRange.x, volumeRange.y);

        audioSource.Play();
    }
}
