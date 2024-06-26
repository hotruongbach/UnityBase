using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EffectSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip plane;
    public AudioClip clickButton;
    public AudioClip confeti;
    public AudioClip correct;
    public AudioClip endPaint;
    public AudioClip penColor;
    public AudioClip win;
    public AudioClip pencil;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip ac, bool setLoop = false)
    {      
        if (PlayerPrefsManager.Sound != false)
        {
            audioSource.clip = ac;
            audioSource.loop = setLoop;
            audioSource.Play();
        }
    }
    public void PauseAudio()
    {
        audioSource.Stop();
        audioSource.loop = false;
    }
}
