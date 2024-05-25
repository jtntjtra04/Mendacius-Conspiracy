using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public GameObject question_option;
    public GameObject command_option;
    public bool command_active;
    public bool question_active;

    // References
    private Worker worker;
    public RandomCall random_call;

    private void Start()
    {
        question_option.SetActive(false);
        question_active = false;
        command_option.SetActive(false);
        command_active = false;
    }
    public void OpenCommand()
    {
        command_option.SetActive(true);
        command_active = true;
    }
    public void OpenQuestion()
    {
        if (question_active)
        {
            CloseQuestion();
        }
        else
        {
            question_option.SetActive(true);
            question_active = true;
        }
    }
    public void CloseQuestion()
    {
        question_option.SetActive(false);
        question_active = false;
    }
    public void EndInteraction()
    {
        worker = FindAnyObjectByType<Worker>();
        command_option.SetActive(false);
        command_active = false;
        worker.StartMovingBack();
        random_call.on_interrogation = false;
    }
}
