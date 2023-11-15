using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

[DefaultExecutionOrder(-10)]

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
          
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Background Music");
    }

    public void Play (string name)
    {
       Sound s = Array.Find(sounds, Sound => Sound.name == name);
       if(s == null)
       {
        Debug.LogWarning("Sound: " + name + "not found!");
        return;
       }
       if(PlayerPrefs.GetInt("muted") != 1)
        s.source.Play();
    }
}