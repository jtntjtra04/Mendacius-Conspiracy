using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSheet : MonoBehaviour
{
    public GameObject tutorial_sheet;
    public GameObject[] sheets;
    private bool tutorial_active = false;
    private int curr_index;

    private void Start()
    {
        tutorial_sheet.SetActive(false);
        sheets[0].SetActive(true);
        for (int i = 1; i < sheets.Length; i++)
        {
            sheets[i].SetActive(false);
        }
    }
    public void OpenTutorial()
    {
        AudioManager.instance.PlaySFX("Click");
        tutorial_sheet.SetActive(true);
        curr_index = 0;
        sheets[curr_index].SetActive(true);
        tutorial_active = true;
    }
    public void CloseTutorial()
    {
        AudioManager.instance.PlaySFX("Click");
        tutorial_sheet.SetActive(false);
        sheets[curr_index ].SetActive(false);
        tutorial_active = false;
    }
    public void RightShift()
    {
        sheets[curr_index].SetActive(false); // Deactivate the current Worker Paper
        curr_index++; // Increment index / shift to right
        if (curr_index >= sheets.Length)
        {
            curr_index = 0;
        }
        sheets[curr_index].SetActive(true); // Activate the next worker paper
        AudioManager.instance.PlaySFX("FlipPage");
    }
    public void LeftShift()
    {
        sheets[curr_index].SetActive(false); // Deactivate the current Worker Paper
        curr_index--; // Decrement index / shift to left
        if (curr_index < 0)
        {
            curr_index = sheets.Length - 1;
        }
        sheets[curr_index].SetActive(true); // Activate the previous worker paper
        AudioManager.instance.PlaySFX("FlipPage");
    }
}
