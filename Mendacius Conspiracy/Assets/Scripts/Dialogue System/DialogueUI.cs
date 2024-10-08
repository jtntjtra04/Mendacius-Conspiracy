using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    //public DialogueManager dialogue_manager;
    public GameObject manager_dialoguebox;
    public GameObject worker_dialoguebox;
    public Text manager_text;
    public Text worker_text;
    public Text worker_name;
    public bool on_dialogue;

    // References
    private CommandManager command_manager;
    public TimeSystem time_system;
    public ActionPoint player_AP;
    public PhoneCall phone_call;
    public RandomCall random_call;

    private void Start()
    {
        on_dialogue = false;
        command_manager = GetComponent<CommandManager>();
    }

    public void QuestionPersonalTrivia()
    {
        if(player_AP.has_AP && !on_dialogue)
        {
            player_AP.UseActionPoint();

            DialogueManager dialogue_manager = FindAnyObjectByType<DialogueManager>();

            on_dialogue = true;
            string current_name = dialogue_manager.GetName();
            DialogueResponse response = dialogue_manager.GetResponse(0);

            StartCoroutine(DisplayResponse(response, current_name));
        }
    }
    public void QuestionRelationships()
    {
        if (player_AP.has_AP && !on_dialogue)
        {
            player_AP.UseActionPoint();

            DialogueManager dialogue_manager = FindAnyObjectByType<DialogueManager>();

            on_dialogue = true;
            string current_name = dialogue_manager.GetName();
            DialogueResponse response = dialogue_manager.GetResponse(1);
            
            StartCoroutine(DisplayResponse(response, current_name));
        }
    }
    public void QuestionOvertimeWork()
    {
        if (player_AP.has_AP && !on_dialogue)
        {
            player_AP.UseActionPoint();

            DialogueManager dialogue_manager = FindAnyObjectByType<DialogueManager>();

            on_dialogue = true;
            string current_name = dialogue_manager.GetName();
            DialogueResponse response = dialogue_manager.GetResponse(2);
            
            StartCoroutine(DisplayResponse(response, current_name));
        }
    }
    public void QuestionWorkExpertise()
    {
        if (player_AP.has_AP && !on_dialogue)
        {
            player_AP.UseActionPoint();

            DialogueManager dialogue_manager = FindAnyObjectByType<DialogueManager>();

            on_dialogue = true;
            string current_name = dialogue_manager.GetName();
            DialogueResponse response = dialogue_manager.GetResponse(3);

            StartCoroutine(DisplayResponse(response, current_name));
        }
    }
    public void QuestionWorkProgress()
    {
        if (player_AP.has_AP && !on_dialogue)
        {
            player_AP.UseActionPoint();

            DialogueManager dialogue_manager = FindAnyObjectByType<DialogueManager>();

            on_dialogue = true;
            string current_name = dialogue_manager.GetName();
            if (time_system.time >= 22 && current_name == "Anna")
            {
                Worker worker = FindAnyObjectByType<Worker>();
                if (worker.is_troll)
                {
                    dialogue_manager.SetState("troll");
                }
                DialogueResponse response = dialogue_manager.GetResponse(5);
                StartCoroutine(DisplayResponse(response, current_name));
            }
            else if (time_system.time >= 23 && current_name == "Haris")
            {
                Worker worker = FindAnyObjectByType<Worker>();
                if (worker.is_troll)
                {
                    dialogue_manager.SetState("troll");
                }
                DialogueResponse response = dialogue_manager.GetResponse(5);
                StartCoroutine(DisplayResponse(response, current_name));
            }
            else if (time_system.time >= 23 && current_name == "Lucia")
            {
                Worker worker = FindAnyObjectByType<Worker>();
                if (worker.is_troll)
                {
                    dialogue_manager.SetState("troll");
                }
                DialogueResponse response = dialogue_manager.GetResponse(5);
                StartCoroutine(DisplayResponse(response, current_name));
            }
            else if (time_system.time >= 22 && current_name == "Desmond")
            {
                Worker worker = FindAnyObjectByType<Worker>();
                if (worker.is_troll)
                {
                    dialogue_manager.SetState("troll");
                }
                DialogueResponse response = dialogue_manager.GetResponse(5);
                StartCoroutine(DisplayResponse(response, current_name));
            }
            else
            {
                DialogueResponse response = dialogue_manager.GetResponse(4);
                StartCoroutine(DisplayResponse(response, current_name));
            }
        }
    }
    private IEnumerator DisplayResponse(DialogueResponse response, string name)
    {
        manager_dialoguebox.SetActive(true);
        manager_text.text = response.question;
        int curr_worker = random_call.GetCurrentWorkerIndex();
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        AudioManager.instance.PlaySFX("Click");

        worker_dialoguebox.SetActive(true);
        worker_name.text = name;
        worker_text.text = response.answer;
        if (curr_worker == 0)
        {
            AudioManager.instance.PlaySFX("Anna");
        }
        else if (curr_worker == 1)
        {
            AudioManager.instance.PlaySFX("Haris");
        }
        else if (curr_worker == 2)
        {
            AudioManager.instance.PlaySFX("Lucia");
        }
        else if (curr_worker == 3)
        {
            AudioManager.instance.PlaySFX("Desmond");
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        AudioManager.instance.PlaySFX("Click");

        manager_text.text = response.special_message;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        AudioManager.instance.PlaySFX("Click");

        manager_dialoguebox.SetActive(false);
        worker_dialoguebox.SetActive(false);
        Debug.Log("Dialogue Done");
        if (!phone_call.on_call)
        {
            time_system.UpdateTime();
        }
        on_dialogue = false;
    }
}
