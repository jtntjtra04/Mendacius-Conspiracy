using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] music_sound, background_sound, sfx_sound, hybrid_sound, horror_sound, yapping_sound;
    public AudioSource music_source, background_source, sfx_source, hybrid_source, horror_source, yapping_source;

    private float max_music_volume = 0.8f;

    private void Awake()
    {
        if (instance == null) // to make things easier to access
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //  Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("Theme"); // When game start play theme
        PlayBGM("Clock");
        PlayHorrorMusic("Ambient"); // Play horror music at low volume
        horror_source.volume = 0f;
    }
    public void PlayMusic(string name) //Call this function from any script u want to add music
    {
        Sound sound = Array.Find(music_sound, x => x.name == name); //Search audio from array
        if (sound != null)
        {
            music_source.clip = sound.clip;
            music_source.Play();
        }
        else
        {
            Debug.Log("Music Not Found");
        }
    }
    public void PlayBGM(string name) //Call this function from any script u want to add music
    {
        Sound sound = Array.Find(background_sound, x => x.name == name); //Search audio from array
        if (sound != null)
        {
            background_source.clip = sound.clip;
            background_source.Play();
        }
        else
        {
            Debug.Log("BGM Not Found");
        }
    }
    public void PlaySFX(string name) //Call this function from any script u want to add SFX
    {
        Sound sound = Array.Find(sfx_sound, x => x.name == name); //Search audio from array
        if (sound != null)
        {
            sfx_source.PlayOneShot(sound.clip, 1f);
        }
        else
        {
            Debug.Log("SFX Not Found");
        }
    }
    public void PlayHybrid(string name)
    {
        Sound sound = Array.Find(hybrid_sound, x => x.name == name); //Search audio from array
        if (sound != null)
        {
            hybrid_source.clip = sound.clip;
            hybrid_source.Play();
        }
        else
        {
            Debug.Log("Hybrid Not Found");
        }
    }
    public void PlayHorrorMusic(string name) //Call this function from any script u want to add music
    {
        Sound sound = Array.Find(horror_sound, x => x.name == name); //Search audio from array
        if (sound != null)
        {
            horror_source.clip = sound.clip;
            horror_source.Play();
        }
        else
        {
            Debug.Log("Horror Not Found");
        }
    }
    public void PlayYapping(string name)
    {
        Sound sound = Array.Find(yapping_sound, x => x.name == name); //Search audio from array
        if (sound != null)
        {
            yapping_source.clip = sound.clip;
            yapping_source.Play();
        }
        else
        {
            Debug.Log("Yapping Not Found");
        }
    }
    public void ToggleMusic()
    {
        music_source.mute = !music_source.mute;
    }
    public void ToggleSFX()
    {
        sfx_source.mute = !sfx_source.mute;
    }
    public void MusicVolume(float volume)
    {
        music_source.volume = Mathf.Min(volume, max_music_volume);
    }
    public void SFXVolume(float volume)
    {
        sfx_source.volume = volume;
    }
    public void DecreaseMusicVolume()
    {
        music_source.volume -= 0.2f;
        max_music_volume = Mathf.Max(0, max_music_volume - 0.2f);
    }
    public void IncreaseHorrorMusicVolume()
    {
        horror_source.volume += 0.075f;
    }
    public void IncreaseVolumeMusic()
    {
        music_source.volume = 0.8f;
    }
    public void PlayHorrorMusicHighVolume()
    {
        horror_source.volume = 0.5f;
    }
    public void ChangeMusic(string name)
    {
        Sound sound = Array.Find(music_sound, x => x.name == name);
        if (sound != null)
        {
            if (music_source.clip == null || music_source.clip == sound.clip)
            {
                Debug.Log("Same Song Played");
                return;
            }
            music_source.Stop();
            music_source.clip = sound.clip;
            music_source.clip.LoadAudioData();
            music_source.Play();
        }
        else
        {
            Debug.Log("Music Not Found");
        }
    }
}
