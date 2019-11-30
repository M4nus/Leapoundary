using System;
using UnityEngine;              
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        Play("Void");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void PlayRandom(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = UnityEngine.Random.Range((s.source.pitch/1.5f), (s.source.pitch * 1.5f));
        s.source.Play();
    }
}
