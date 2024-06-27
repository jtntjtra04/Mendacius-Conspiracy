using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // Slider
    [Header("Slider")]
    public Slider music_slider;
    public Slider sfx_slider;

    // Toggle mute image 
    [Header("Image")]
    public Sprite music_mute_image;
    public Sprite sfx_mute_image;

    // Button Reference
    [Header("Button")]
    public Button music_button;
    public Button sfx_button;

    private Sprite music_image;
    private Sprite sfx_image;
    private bool music_on = true;
    private bool sfx_on = true;

    // References
    public GameObject pause_menu;

    private void Start()
    {
        music_image = music_button.image.sprite;
        sfx_image = sfx_button.image.sprite;
        music_slider.value = 1f;
        sfx_slider.value = 1f;
    }
    public void MusicButton()
    {
        AudioManager.instance.PlaySFX("Click");
        if (music_on)
        {
            music_button.image.sprite = music_mute_image;
            music_on = false;
        }
        else
        {
            music_button.image.sprite = music_image;
            music_on = true;
        }
        AudioManager.instance.ToggleMusic();
    }
    public void SFXButton()
    {
        AudioManager.instance.PlaySFX("Click");
        if (sfx_on)
        {
            sfx_button.image.sprite = sfx_mute_image;
            sfx_on = false;
        }
        else
        {
            sfx_button.image.sprite = sfx_image;
            sfx_on = true;
        }
        AudioManager.instance.ToggleSFX();
    }
    public void MusicSlider()
    {
        AudioManager.instance.MusicVolume(music_slider.value);
    }
    public void SFXSlider()
    {
        AudioManager.instance.SFXVolume(sfx_slider.value);
    }
    public void BackButton()
    {
        AudioManager.instance.PlaySFX("Click");
        pause_menu.SetActive(true);
        gameObject.SetActive(false);
    }
}
