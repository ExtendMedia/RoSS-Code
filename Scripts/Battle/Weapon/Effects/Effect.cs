using UnityEngine;

/// <summary>
/// Effect class 
/// </summary>
public class Effect : MonoBehaviour
{
    AudioSource _audioSource;
    AudioClip _audioClip;

    public void SetAudioSource(AudioSource audioSource) => _audioSource = audioSource;

    public void SetAudioClip(AudioClip audioClip) => _audioClip = audioClip;

    void PlaySound()
    {
        _audioSource.PlayOneShot(_audioClip);
    }

    public void Init(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
        PlaySound();
    }
}
