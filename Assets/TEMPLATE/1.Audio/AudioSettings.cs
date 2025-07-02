using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template.Audio
{
    public class AudioSettings
    {
        public bool EnableMusic;
        public bool EnableSfx;
        public bool EnableVibrate;
        public float MasterVolume;
        public AudioSettings()
        {
            EnableMusic = true;
            EnableSfx = true;
            EnableVibrate = true;
            MasterVolume = 1f;
        }
    }
}