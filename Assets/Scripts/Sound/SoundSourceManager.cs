using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SpriteEffecDictionary : SerializableDictionaryBase<SoundType, Sprite>
{

}

[System.Serializable]
public class DefaultSoundDictionary : SerializableDictionaryBase<SoundType, AudioClip>
{

}

[CreateAssetMenu(fileName = "SoundSourceManager", menuName ="Manager/SoundSourceManager")]
public class SoundSourceManager : SingletonScriptableObject<SoundSourceManager>
{
    [Header("Sounds")]
    public SpriteEffecDictionary spriteEffec;
    public DefaultSoundDictionary defaultSound;
    public AudioClip GetSoundWithType(SoundType type)
    {
        return defaultSound[type];
    }
    public Sprite GetSpriteType(SoundType type)
    {
        return spriteEffec[type];
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SoundSourceManager))]
public class SoundSourceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        return;
    }
}
#endif