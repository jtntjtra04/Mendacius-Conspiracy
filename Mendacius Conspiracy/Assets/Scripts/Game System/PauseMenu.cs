using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pause_menu;
    public SettingsMenu settings_menu;
    public static bool game_paused = false;

    // References
    public ChangeScene change_scene;
    private Credibility cred;

    private void Awake()
    {
        cred = GetComponent<Credibility>();
    }
    private void Start()
    {
        ResumeGame();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && cred.credibility > 0)
        {
            if (game_paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void ResumeGame()
    {
        pause_menu.SetActive(false);
        settings_menu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        game_paused = false;
    }
    public void PauseGame()
    {
        pause_menu.SetActive(true);
        Time.timeScale = 0f;
        game_paused = true;
    }
    public void SettingsMenu()
    {
        AudioManager.instance.PlaySFX("Click");
        settings_menu.gameObject.SetActive(true);
        pause_menu.SetActive(false);
        Time.timeScale = 0f;
        game_paused = true;
    }
    public void MainMenu()
    {
        AudioManager.instance.PlaySFX("Click");
        pause_menu.SetActive(false);
        Time.timeScale = 1.0f;
        game_paused = false;
        DataManager.instance.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }
}
