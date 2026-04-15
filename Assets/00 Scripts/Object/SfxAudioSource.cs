using UnityEngine;
public class SfxAudioSource : PoolingObject
{
    public AudioSource audioSource;

    public void PlaySfx(AudioClip clip, float volume = 1f)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        StartCoroutine(IEDespawn(clip.length));
    }
}