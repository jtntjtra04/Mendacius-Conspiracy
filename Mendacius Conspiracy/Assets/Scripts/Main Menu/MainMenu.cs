using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]

    [SerializeField] private Button continuegame_button;

    [Header("Menu")]
    [SerializeField] private GameObject settings_menu;
    [SerializeField] private GameObject credits_menu;

    public Animator transition_fade;
    private void Start()
    {
        settings_menu.SetActive(false);
        credits_menu.SetActive(false);
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
    public void GoToCreditsMenu()
    {
        MainMenuAudioManager.instance.PlaySFX("Click");
        credits_menu.SetActive(true);
    }
    public void GoToSettingsMenu()
    {
        MainMenuAudioManager.instance.PlaySFX("Click");
        settings_menu.SetActive(true);
    }
    public void BackToMainMenu()
    {
        MainMenuAudioManager.instance.PlaySFX("Click");
        credits_menu.SetActive(false);
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
