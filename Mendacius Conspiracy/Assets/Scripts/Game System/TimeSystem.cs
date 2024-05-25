using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    // Days
    public int day;
    public Text day_number;

    // Time
    public int time;
    public Text time_number;

    // Transition
    [SerializeField] private int transition_duration;
    public Animator transition_image;
    public Text transition_day;

    // UI Interactablility Control
    public List<CanvasGroup> all_UI;
    public List<CanvasGroup> exception_UI;

    // Button
    public GameObject endshift_button;

    // Action Point and Credibility
    private ActionPoint AP;

    private void Awake()
    {
        AP = GetComponent<ActionPoint>();
    }
    private void Start()
    {
        endshift_button.SetActive(false);
        day = 1;
        time = 16;

        time_number.text = time.ToString() + ":00";
        day_number.text = day.ToString();
    }

    public void UpdateTime()
    {
        time += 1;
        if(time >= 24 && day < 8)
        {
            time = 0;
            day += 1;
            time_number.text = time.ToString() + "0:00";
            day_number.text = day.ToString();
            endshift_button.SetActive(true);
            SetAllUIInteractable(false); // Disable Interaction to All UI (Except exception_UI)
            return;
        }
        else if (day >= 8)
        {
            //Next step (END GAME)
        }
        time_number.text = time.ToString() + ":00";
    }
    public void EndShift()
    {
        endshift_button.SetActive(false);
        ResetTime();
        AP.ResetActionPoint();
        StartCoroutine(UpdateDay());
    }
    private IEnumerator UpdateDay()
    {
        SetExceptionalUIInteractable(false); // Disable exception_UI Interactions

        transition_day.text = "Day " + day.ToString();
        transition_image.Play("StartTransition");
        yield return new WaitForSeconds(transition_duration);
        day_number.text = day.ToString();
        time_number.text = time.ToString() + ":00";
        transition_image.Play("EndTransition");

        SetAllUIInteractable(true); // Enable Interactions
        SetExceptionalUIInteractable(true);
    }
    public void ResetTime()
    {
        time = 16;
    }
    private void SetAllUIInteractable(bool interactable)
    {
        foreach (var UI in all_UI)
        {
            UI.interactable = interactable;
        }
    }
    private void SetExceptionalUIInteractable(bool interactable)
    {
        foreach (var UI in exception_UI)
        {
            UI.interactable = interactable;
        }
    }
}
