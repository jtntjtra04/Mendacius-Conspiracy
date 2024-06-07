using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCamera : MonoBehaviour
{
    public Camera workdesk_camera;
    public Camera catchgame_camera;
    public Animator fade_image;
    public int transition_duration;
    public GameObject catchgame_UI;
    public GameObject start_panel;
    public GameObject end_panel;
    private bool is_cg = false;

    // References
    public PlayCatchGame catch_game;
    public ChangeScene change_scene;
    public PhoneCall phone_call;
    public TimeSystem time_system;
    public DailyQuota daily_quota;
    public Text target_score;
    //public ScoreManager score_manager;
    private void Start()
    {
        workdesk_camera.depth = 1;
        catchgame_camera.depth = 0;
        catchgame_UI.SetActive(false);
    }
    public void SwitchToCatchGameCamera()
    {
        if(daily_quota.curr_catch < daily_quota.daily_catch)
        {
            change_scene.monitor_active = false;
            is_cg = true;
            StartCoroutine(CameraTransition());
        }
        else
        {
            Notification.Instance.AddQueue("Not Available yet");
        }
    }

    private IEnumerator CameraTransition()
    {
        fade_image.Play("Start_Fade"); // Start Transition
        yield return new WaitForSeconds(transition_duration);
        workdesk_camera.depth = 0;
        catchgame_camera.depth = 1; // Switch Camera
        catchgame_UI.SetActive(true);
        target_score.text = ScoreManager.instance.target_score.ToString(); // Set Up target score UI in Catch Game UI
        start_panel.SetActive(true);
        end_panel.SetActive(false);
        fade_image.Play("End_Fade"); // EndTransition
    }
    public void SwitchToMainCamera()
    {
        StartCoroutine(BackCameraTransition());
    }
    private IEnumerator BackCameraTransition()
    {
        fade_image.Play("Start_Fade"); // Start Transition
        yield return new WaitForSeconds(transition_duration);
        workdesk_camera.depth = 1;
        catchgame_camera.depth = 0; // Switch Camera
        catchgame_UI.SetActive(false);
        start_panel.SetActive(false);
        end_panel.SetActive(false);
        change_scene.monitor_active = true;
        catch_game.catchgame_active = false;
        is_cg = false;
        fade_image.Play("End_Fade"); // End Transition
    }
    public void AfterGame()
    {
        StartCoroutine(AfterGameBackCamera());
    }
    private IEnumerator AfterGameBackCamera()
    {
        fade_image.Play("Start_Fade"); // Start Transition
        yield return new WaitForSeconds(transition_duration);
        workdesk_camera.depth = 1;
        catchgame_camera.depth = 0; // Switch Camera
        catchgame_UI.SetActive(false);
        start_panel.SetActive(false);
        end_panel.SetActive(false);
        change_scene.monitor_active = true;
        catch_game.catchgame_active = false;
        is_cg = false;
        daily_quota.curr_catch = 1;
        daily_quota.UpdateInfoCatchText();
        if (!phone_call.on_call)
        {
            time_system.UpdateTime();
        }
        fade_image.Play("End_Fade"); // End Transition
    }
    public void ReturnButton()
    {
        if (is_cg && !catch_game.catchgame_active)
        {
            SwitchToMainCamera();
        }
    }
}
