using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioSource[] channels;

    public static SoundManager instance;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            print("Already a Sound Manager Instantiated");
            Destroy(gameObject);
        }

        channels = new AudioSource[sounds.Length];
        for (int i = 0; i < channels.Length; i++)
        {
            channels[i] = gameObject.AddComponent<AudioSource>();
            channels[i].clip = sounds[i];
        }
    }

    void Start()
    {
       
    }

    public void Play(SoundID id, bool loop = false, float volume = 1, float pitch = 1)
    {
        channels[(int)id].Play();
        channels[(int)id].loop = loop;
        channels[(int)id].volume = volume;
        channels[(int)id].pitch = pitch;

    }

    public void StopAll()
    {
        foreach (var t in channels)
        {
            t.Stop();
        }
    }

    public void Stop(SoundID id)
    {
        channels[(int)id].Stop();
    }

    public void Pause(SoundID id)
    {
        channels[(int)id].Pause();
    }

    public void Resume(SoundID id)
    {
        channels[(int)id].UnPause();
    }

    public void ToggleMute(SoundID id)
    {
        channels[(int)id].mute = !channels[(int)id].mute;
    }

    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}

    public enum SoundID 
    {
        Spider
    }
