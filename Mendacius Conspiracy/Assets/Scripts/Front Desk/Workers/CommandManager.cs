using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandManager : MonoBehaviour
{
    public GameObject question_option;
    public GameObject command_option;
    public GameObject confirmation_window;
    public bool command_active;
    public bool question_active;
    public GameObject worker_fired;
    private bool ending;

    // References
    private Worker worker;
    public RandomCall random_call;
    private DialogueUI dialogue_UI;
    public PhoneCall phone_call;
    public TimeSystem time_system;
    public Animator fade_transition;
    public Animator ending_scene;
    public PostProcessing post_processing;

    private void Awake()
    {
        dialogue_UI = GetComponent<DialogueUI>();
    }
    private void Start()
    {
        question_option.SetActive(false);
        question_active = false;
        command_option.SetActive(false);
        command_active = false;
        confirmation_window.SetActive(false);
        worker_fired.SetActive(false);
        ending = false;
    }
    public void OpenCommand()
    {
        command_option.SetActive(true);
        command_active = true;
        CloseQuestion();
    }
    public void OpenQuestion()
    {
        AudioManager.instance.PlaySFX("Click");
        question_option.SetActive(true);
        question_active = true;
        CloseCommand();
    }
    public void CloseQuestion()
    {
        question_option.SetActive(false);
        question_active = false;
    }
    public void CloseCommand()
    {
        command_option.SetActive(false);
        command_active = false;
    }
    public void FireButton()
    {
        AudioManager.instance.PlaySFX("Click");
        confirmation_window.SetActive(true);
    }
    public void FireWorker()
    {
        AudioManager.instance.PlaySFX("Click");
        confirmation_window.SetActive(false);

        int curr_worker = random_call.GetCurrentWorkerIndex();

        if (curr_worker == random_call.troll_index)
        {
            StartCoroutine(GoodEnding());
        }
        else
        {
            StartCoroutine(BadEnding());
        }
    }
    public void CloseConfirmationWindow()
    {
        AudioManager.instance.PlaySFX("Click");
        confirmation_window.SetActive(false);
    }
    public void TriggerNormalEnding()
    {
        StartCoroutine(NormalEnding());
    }
    public void EndInteraction()
    {
        worker = FindAnyObjectByType<Worker>();
        CloseQuestion();
        CloseCommand();
        dialogue_UI.StopAllCoroutines();
        dialogue_UI.manager_dialoguebox.SetActive(false);
        dialogue_UI.worker_dialoguebox.SetActive(false);

        if (worker != null)
        {
            if (worker.current_worker == 0 && !ending)
            {
                AudioManager.instance.PlaySFX("AnnaBye");
            }
            else if (worker.current_worker == 1 && !ending)
            {
                AudioManager.instance.PlaySFX("HarisBye");
            }
            else if (worker.current_worker == 2 && !ending)
            {
                AudioManager.instance.PlaySFX("LuciaBye");
            }
            else if (worker.current_worker == 3 && !ending)
            {
                AudioManager.instance.PlaySFX("DesmondBye");
            }
            worker.StartMovingBack();
        }
        if (dialogue_UI.on_dialogue && !phone_call.on_call)
        {
            time_system.UpdateTime();
        }
        random_call.on_interrogation = false;
        dialogue_UI.on_dialogue = false;
    }
    private IEnumerator GoodEnding()
    {
        AudioManager.instance.music_source.Stop();
        yield return new WaitForSeconds(3f);
        worker_fired.SetActive(true);
        ending = true;
        EndInteraction();
        yield return new WaitForSeconds(3f);
        worker_fired.SetActive(false);
        ending = false;
        fade_transition.Play("Start_Fade");
        yield return new WaitForSeconds(4f);
        ending_scene.Play("GoodEnding_Start");

        while(!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        ending_scene.Play("GoodEnding_End");
        SceneManager.LoadScene("MainMenu");
    }
    private IEnumerator NormalEnding()
    {
        AudioManager.instance.music_source.Stop();
        yield return new WaitForSeconds(6f);
        fade_transition.Play("Start_Fade");
        yield return new WaitForSeconds(4f);
        ending_scene.Play("NormalEnding_Start");

        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        ending_scene.Play("NormalEnding_End");
        SceneManager.LoadScene("MainMenu");
    }
    private IEnumerator BadEnding()
    {
        AudioManager.instance.music_source.Stop();
        yield return new WaitForSeconds(3f);
        worker_fired.SetActive(true);
        post_processing.ChangeRedScene();
        ending = true;
        EndInteraction();
        yield return new WaitForSeconds(3f);
        worker_fired.SetActive(false);
        ending = false;
        fade_transition.Play("Start_Fade");
        post_processing.ResetColorScene();
        yield return new WaitForSeconds(4f);
        ending_scene.Play("BadEnding_Start");

        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        ending_scene.Play("BadEnding_End");
        SceneManager.LoadScene("MainMenu");
    }
}
