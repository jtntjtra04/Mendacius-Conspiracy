using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public DialogueData dialogue_data;
    private DialogueState worker_state;
    private DialogueResponse worker_response;
    private string character_name;

    private void Start()
    {
        character_name = dialogue_data.character_name;
    }
    public void SetState(string state)
    {
        switch(state)
        {
            case "normal":
                worker_state = dialogue_data.normal_state;
                break;
            case "tired":
                worker_state = dialogue_data.tired_state;
                break;
            case "troll":
                Debug.Log("Worker state change to troll");
                worker_state = dialogue_data.troll_state;
                break;
        }
    }
    public void SetWorkerState(string state)
    {
        SetState(state);
    }
    
    public DialogueResponse GetResponse(int index)
    {
        if(index >= 0 && index < worker_state.responses.Length)
        {
            worker_response = worker_state.responses[index];
            return worker_response;
        }
        return null;
    }
    public string GetName()
    {
        return character_name;
    }
}
