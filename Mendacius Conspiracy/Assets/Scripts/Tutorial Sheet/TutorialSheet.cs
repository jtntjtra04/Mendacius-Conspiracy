using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSheet : MonoBehaviour
{
    public GameObject tutorial_sheet;
    private bool tutorial_active = false;

    private void Start()
    {
        tutorial_sheet.SetActive(false);
    }
    public void OpenTutorial()
    {
        tutorial_sheet.SetActive(true);
        tutorial_active = true;
    }
    public void CloseTutorial()
    {
        tutorial_sheet.SetActive(false);
        tutorial_active = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (tutorial_active)
            {
                CloseTutorial();
            }
        }
    }
}
