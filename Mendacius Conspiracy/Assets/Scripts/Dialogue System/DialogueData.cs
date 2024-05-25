using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    public string character_name;
    public DialogueState normal_state;
    public DialogueState tired_state;
    public DialogueState troll_state;
}
[System.Serializable]
public class DialogueState
{
    public DialogueResponse[] responses;
}
[System.Serializable]
public class DialogueResponse
{
    public string question;
    public string answer;
    public string special_message;
}
