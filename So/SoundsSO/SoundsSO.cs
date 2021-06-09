#if UNITY_EDITOR

/* 
 * author: https://github.com/AdrianNowak97
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


[CreateAssetMenu(menuName = "SoundsSO")]
public class SoundsSO : ScriptableObject
{
    public List<CharacterSounds> characterSounds = new List<CharacterSounds>() { new CharacterSounds("default",0.1f,new List<AudioClip>()) };
}

[Serializable]
public class CharacterSounds
{
    public string name;
    public float speed;
    public List<AudioClip> AudioClips;

    public CharacterSounds(string name, float speed, List<AudioClip> audioClips)
    {
        this.name = name;
        this.speed = speed;
        this.AudioClips = new List<AudioClip>() { null };
    }

}
#endif
