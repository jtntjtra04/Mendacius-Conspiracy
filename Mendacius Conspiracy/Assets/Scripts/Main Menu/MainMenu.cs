using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]

    [SerializeField] private Button continuegame_button;

    public Animator transition_fade;
    private void Start()
    {
        Debug.Log("Start Main Menu");
        transition_fade.Play("Default_End");
        if (!DataManager.instance.HasGameData())
        {
            continuegame_button.interactable = false;
        }
        else
        {
            continuegame_button.interactable = true;
        }
    }
    public void GoToNewGame()
    {
        MainMenuAudioManager.instance.PlaySFX("Click");
        DataManager.instance.NewGame();
        StartCoroutine(NewGameTransition());
    }
    public void ContinueGame()
    {
        MainMenuAudioManager.instance.PlaySFX("Click");
        DataManager.instance.SaveGame();
        StartCoroutine(ContinueGameTransition());
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
    private IEnumerator NewGameTransition()
    {
        transition_fade.Play("Start_Fade");
        MainMenuAudioManager.instance.music_source.Stop();
        yield return new WaitForSeconds(3f);
        DataManager.instance.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transition_fade.Play("End_Fade");
    }
    private IEnumerator ContinueGameTransition()
    {
        transition_fade.Play("Start_Fade");
        MainMenuAudioManager.instance.music_source.Stop();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transition_fade.Play("End_Fade");
    }
}
