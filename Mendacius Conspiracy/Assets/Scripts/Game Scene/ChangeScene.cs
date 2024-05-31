using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public Camera frontdesk_camera;
    public Camera workdesk_camera;
    public GameObject front_desk;
    public GameObject work_desk;
    public GameObject fd_button;
    public GameObject wd_button;
    public GameObject monitor;
    public bool monitor_active = false;

    // References
    public PlayCatchGame catch_game;

    // UI Interactability Control
    public CanvasGroup workdesk_UIs;
    public CanvasGroup frontdesk_UIs;
    private void Start()
    {
        workdesk_camera.depth = 0;
        frontdesk_camera.depth = 1;
        front_desk.SetActive(true);
        work_desk.SetActive(true);
        monitor.SetActive(false);
        wd_button.SetActive(true);
        fd_button.SetActive(false);

        frontdesk_UIs.interactable = true;
        workdesk_UIs.interactable = false;

    }
    public void ChangeToFrontDesk()
    {
        AudioManager.instance.PlaySFX("ClickSpace");
        workdesk_camera.depth = 0;
        frontdesk_camera.depth = 1;
        /*work_desk.SetActive(false);
        front_desk.SetActive(true);*/
        fd_button.SetActive(false);
        wd_button.SetActive(true);

        frontdesk_UIs.interactable = true;
        workdesk_UIs.interactable = false;
    }
    public void ChangeToWorkDesk()
    {
        AudioManager.instance.PlaySFX("ClickSpace");
        frontdesk_camera.depth = 0;
        workdesk_camera.depth = 1;
        //front_desk.SetActive(false);
        monitor.SetActive(false);
        work_desk.SetActive(true);
        wd_button .SetActive(false);
        fd_button.SetActive(true);
        monitor_active = false;

        frontdesk_UIs.interactable = false;
        workdesk_UIs.interactable = true;
    }
    public void ChangeToMonitor()
    {
        AudioManager.instance.PlaySFX("ClickSpace");
        monitor.SetActive(true);
        //work_desk.SetActive(false);
        wd_button.SetActive(false);
        fd_button.SetActive(false);
        monitor_active = true;
        
        workdesk_UIs .interactable = false;
    }
    public void ResetScene()
    {
        monitor.SetActive(false);
        monitor_active = false;
        ChangeToFrontDesk();
    }
    public void ReturnButton()
    {
        if (monitor_active)
        {
            Debug.Log("Change to work desk");
            ChangeToWorkDesk();
        }
    }
}
