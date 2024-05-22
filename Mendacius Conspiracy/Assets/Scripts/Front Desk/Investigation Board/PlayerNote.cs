using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNote : MonoBehaviour
{
    public GameObject note;
    public GameObject board;

    public void AddNote()
    {
        Instantiate(note, board.transform);
    }
}
