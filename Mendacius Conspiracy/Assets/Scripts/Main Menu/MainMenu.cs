using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition_fade;
    private void Start()
    {
        transition_fade.Play("Default_End");
    }
    public void PlayGame()
    {
        MainMenuAudioManager.instance.PlaySFX("Click");
        StartCoroutine(PlayTransition());
    }
    public void GoToSettingsMenu()
    {
        MainMenuAudioManager.instance.PlaySFX("Click");
        SceneManager.LoadScene("SettingsMenu");
    }
    public void ExitGame()
    {
        MainMenuAudioManager.instance.PlaySFX("Click");
        Application.Quit();
    }
    private IEnumerator PlayTransition()
    {
        transition_fade.Play("Start_Fade");
        MainMenuAudioManager.instance.music_source.Stop();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transition_fade.Play("End_Fade");
    }

}
