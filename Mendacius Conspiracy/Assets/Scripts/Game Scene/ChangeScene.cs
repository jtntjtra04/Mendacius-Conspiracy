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
    private void Start()
    {
        workdesk_camera.depth = 0;
        frontdesk_camera.depth = 1;
        front_desk.SetActive(true);
        work_desk.SetActive(true);
        monitor.SetActive(false);
        wd_button.SetActive(true);
        fd_button.SetActive(false);
    }
    public void ChangeToFrontDesk()
    {
        workdesk_camera.depth = 0;
        frontdesk_camera.depth = 1;
        /*work_desk.SetActive(false);
        front_desk.SetActive(true);*/
        fd_button.SetActive(false);
        wd_button.SetActive(true);
    }
    public void ChangeToWorkDesk()
    {
        frontdesk_camera.depth = 0;
        workdesk_camera.depth = 1;
        //front_desk.SetActive(false);
        monitor.SetActive(false);
        work_desk.SetActive(true);
        wd_button .SetActive(false);
        fd_button.SetActive(true);
        monitor_active = false;
    }
    public void ChangeToMonitor()
    {
        monitor.SetActive(true);
        work_desk.SetActive(false);
        wd_button.SetActive(false);
        fd_button.SetActive(false);
        monitor_active = true;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(monitor_active)
            {
                ChangeToWorkDesk();
            }
        }
    }
}
