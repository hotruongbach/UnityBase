using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BGSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip SoundMain;
    public AudioClip SoundGame;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayMusic(AudioClip ac)
    {
        audioSource.clip = ac;
        if (PlayerPrefsManager.Music == true)
        {
            Play();
        }
    }
    public void Play()
    {
        audioSource.Play();
        audioSource.loop = true;
    }
    public void Stop()
    {
        audioSource.Stop();
        audioSource.loop = false;
    }
}
