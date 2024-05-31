using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNote : MonoBehaviour
{
    public GameObject note_1;
    public GameObject note_2;
    public GameObject board;

    public void AddNote1()
    {
        AudioManager.instance.PlaySFX("Click");
        Instantiate(note_1, board.transform);
    }
    public void AddNote2()
    {
        AudioManager.instance.PlaySFX("Click");
        Instantiate (note_2, board.transform);
    }
}
