using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

[CreateAssetMenu(fileName = "NewSound", menuName = "ScriptableObject/Audio/Sounds", order = 1)]
public class Sound : ScriptableObject
{
    public AudioClip audio;

    [Range(0.1f, 3f)]
    public float pitch;
    [Range(0f, 3f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;

    public bool random;

    public bool looping;
}
