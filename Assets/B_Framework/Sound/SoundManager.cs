using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public BGSounds Music;
    public EffectSound Sound;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void PlayMusic()
    {
        if (PlayerPrefsManager.Music == true)
        {
            Music.PlayMusic(Music.SoundMain);
            Music.Play();
        }
    }
    public void StopMusic()
    {
        Music.Stop();
    }
}
