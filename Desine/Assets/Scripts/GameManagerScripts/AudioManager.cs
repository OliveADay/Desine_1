using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    [HideInInspector]
    public static float SFXvolume = 1;
    [HideInInspector]
    public static float MVolume = 1;
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetFloat("SFX") >= 0.1f)
        {
            SFXvolume = PlayerPrefs.GetFloat("SFX") - 0.1f;
        }
        else if(PlayerPrefs.GetFloat("M") >= 0.1f)
        {
            MVolume = PlayerPrefs.GetFloat("M") - 0.1f;
        }
        foreach(Sound s in sounds)
        {
            if (s.source == null)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.audio;
                s.source.loop = s.looping;

                if (s.looping)
                {
                    s.source.volume = s.volume * MVolume;
                }
                else
                {
                    s.source.volume = s.volume * SFXvolume;
                }
                s.source.pitch = s.pitch;
                
            }
        }
    }
    private void Update()
    {
        foreach (Sound s in sounds)
        {
            if (s.source != null)
            {
                if (s.looping)
                {
                    s.source.volume = s.volume * MVolume;
                }
                else
                {
                    s.source.volume = s.volume * SFXvolume;
                }
            }
            else
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.audio;
                s.source.loop = s.looping;

                if (s.looping)
                {
                    s.source.volume = s.volume * MVolume;
                }
                else
                {
                    s.source.volume = s.volume * SFXvolume;
                }
                s.source.pitch = s.pitch;
            }
        }
    }
    public Sound Random(Sound[] sounds)
    {
        Sound sound = sounds[UnityEngine.Random.Range(0, sounds.Length)];

        sound.source.pitch = UnityEngine.Random.Range(0.5f, 2f);

        return sound;
    }
    public void Play(Sound name)
    {
        print(name.name + " is being played by " + gameObject.name);
        Sound s = Array.Find(sounds, sound => sound.name == name.name);
        if (s.random)
        {
            Sound[] ss = new Sound[1];
            ss[0] = s;
            
            Random(ss);
        }
        s.source.Play();
    }
}
