using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public static AudioManager instance;

    private float _soundVolume = 1f;
    [SerializeField] private float normalizeMusic = 0.4f;

    private void Awake() { instance = this; }

    private void Start()
    {
        PlayMusic("MainTheme");
    }

    private void Update()
    {
        musicSource.volume = _soundVolume * normalizeMusic;
        sfxSource.volume = _soundVolume;
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null )
        {
            Debug.Log("No sound found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("No sound found");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public void ToggleSound()
    {
        musicSource.mute = !musicSource.mute;
        sfxSource.mute = !sfxSource.mute;
    }

    public void SoundVolume(float volume)
    {
        //musicSource.volume = volume;
        //sfxSource.volume = volume;
        _soundVolume = volume;
    }

    //public void SetVolume(float vol)
    //{
    //    _soundVolume = vol;
    //}
}
