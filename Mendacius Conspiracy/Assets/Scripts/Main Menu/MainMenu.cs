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
        StartCoroutine(PlayTransition());
    }
    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private IEnumerator PlayTransition()
    {
        transition_fade.Play("Start_Fade");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transition_fade.Play("End_Fade");
    }

}
