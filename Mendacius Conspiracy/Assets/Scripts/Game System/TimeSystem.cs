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
    public Text penalty_text;

    // UI Interactablility Control
    public List<CanvasGroup> all_UI;
    public List<CanvasGroup> exception_UI;

    // Button
    public GameObject endshift_button;

    // Action Point and Credibility
    private ActionPoint AP;
    private Credibility cred;

    // Change Scene References
    public ChangeScene change_scene;

    // Daily Quota References
    public DailyQuota daily_quota;

    // Other References
    public ArticleManager article_manager;

    private void Awake()
    {
        AP = GetComponent<ActionPoint>();
        cred = GetComponent<Credibility>();
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
        article_manager.on_article = false;
        yield return new WaitForSeconds(transition_duration);
        
        day_number.text = day.ToString();
        time_number.text = time.ToString() + ":00";
        
        transition_image.Play("EndTransition");
        DailyQuotaJudgement(); // Check Daily Quota
        change_scene.ResetScene(); // Back to front desk
        daily_quota.UpdateDailyQuota(day);
        ScoreManager.instance.UpdateTargetScore(day);

        SetAllUIInteractable(true); // Enable Interactions
        SetExceptionalUIInteractable(true);
    }
    private void DailyQuotaJudgement()
    {
        if(daily_quota.curr_fact < daily_quota.daily_fact || daily_quota.curr_catch < daily_quota.daily_catch)
        {
            cred.MinusCredibility(1);
            penalty_text.text = "Daily Quota not met\r\nPenalty : -1 credibility";
        }
        else
        {
            penalty_text.text = "";
        }
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
