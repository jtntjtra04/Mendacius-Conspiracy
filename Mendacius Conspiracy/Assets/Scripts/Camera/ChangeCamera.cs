using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera main_camera;
    public Camera catchgame_camera;
    public Animator fade_image;
    public int transition_duration;
    public GameObject catchgame_UI;
    public GameObject start_panel;
    public GameObject end_panel;
    private void Start()
    {
        catchgame_UI.SetActive(false);
    }
    public void SwitchToCatchGameCamera()
    {
        StartCoroutine(CameraTransition());
    }

    private IEnumerator CameraTransition()
    {
        fade_image.Play("Start_Fade"); // Start Transition
        yield return new WaitForSeconds(transition_duration);
        main_camera.depth = 0;
        catchgame_camera.depth = 1; // Switch Camera
        catchgame_UI.SetActive(true);
        start_panel.SetActive(true);
        end_panel.SetActive(false);
        fade_image.Play("End_Fade"); // EndTransition
    } 
}
