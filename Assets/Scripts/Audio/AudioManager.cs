using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;

    public static AudioManager Instance { get; private set; }

    private void OnEnable()
    {
        if (Instance != null)
            Destroy(this);

        if (Instance == null)
            Instance = this;

        if (Instance == this)
            DontDestroyOnLoad(this);
        else
            Destroy(this);

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        if (Instance == this)
            Instance = null;
    }

    public void PlayClip(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void ChangeMainClip(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
