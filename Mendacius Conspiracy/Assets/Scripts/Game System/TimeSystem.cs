using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour, IDataManager
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
    public Text transition_time;
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

    // UI References
    public InvestigationBoard board;
    public WorkersData workers_data;
    public TutorialSheet tutorial;
    public Archive archive;

    // Other References
    public ArticleManager article_manager;
    public Monitor monitor;
    public DailyNewsManager daily_news;
    public CommandManager command_manager;
    public PhoneCall phone_call;

    private void Awake()
    {
        AP = GetComponent<ActionPoint>();
        cred = GetComponent<Credibility>();
    }
    private void Start()
    {
        endshift_button.SetActive(false);
        //day = 1;
        //time = 16;
        //daily_quota.UpdateDailyQuota(day);
        daily_quota.UpdateFactCheckText();
        daily_quota.UpdateInfoCatchText();

        time_number.text = time.ToString() + ":00";
        day_number.text = day.ToString();
        transition_day.text = "Day " + day.ToString();
        transition_time.text = time.ToString() + ":00";
    }
    public void LoadData(GameData data)
    {
        this.day = data.day;
        this.time = data.time;
        Debug.Log("Loaded day : " + this.day);
        daily_quota.UpdateDailyQuota(day);
        daily_quota.curr_fact = data.curr_fact;
        daily_quota.curr_catch = data.curr_catch;
    }
    public void SaveData(GameData data)
    {
        data.day = this.day;
        data.time = this.time;
        data.curr_fact = daily_quota.curr_fact;
        data.curr_catch = daily_quota.curr_catch;
    }
    public void UpdateTime()
    {
        Debug.Log("Before update - Time: " + time + ", Day: " + day);
        time += 1;
        Debug.Log("After increment - Time: " + time + ", Day: " + day);
        if (time >= 24)
        {
            time = 0;
            day += 1;
            time_number.text = "0:00";
            day_number.text = day.ToString();
            command_manager.EndInteraction();
            endshift_button.SetActive(true);
            Debug.Log("Midnight - Time: " + time + ", Day: " + day);

            if (change_scene.monitor_active)
            {
                change_scene.ChangeToWorkDesk();
            }
            SetAllUIInteractable(false); // Disable Interaction to All UI (Except exception_UI)
            return;
        }
        time_number.text = time.ToString() + ":00";
        Debug.Log("Updated time - Time: " + time_number.text + ", Day: " + day);
    }
    public void EndShift()
    {
        if(cred.credibility <= 0)
        {
            return;
        }
        if (day >= 8)
        {
            command_manager.TriggerNormalEnding();
            return;
        }
        AP.StopAllCoroutines();
        phone_call.StopAllCoroutines();
        foreach (GameObject jumpscare in AP.jumpscares)
        {
            jumpscare.SetActive(false);
        }
        endshift_button.SetActive(false);
        ResetTime();
        AP.ResetActionPoint();
        StartCoroutine(UpdateDay());
    }
    private IEnumerator UpdateDay()
    {
        SetExceptionalUIInteractable(false); // Disable exception_UI Interactions

        transition_day.text = "Day " + day.ToString();
        transition_time.text = time.ToString() + ":00";
        DailyQuotaJudgement(); // Check Daily Quota
        transition_image.Play("StartTransition");
        AudioManager.instance.music_source.Stop();
        AudioManager.instance.horror_source.Stop();
        AudioManager.instance.sfx_source.Stop();
        article_manager.on_article = false;
        //monitor.completed_text.SetActive(false);
        monitor.CloseAllMonitor();
        board.CloseBoard();
        workers_data.CloseWorkersData();
        tutorial.CloseTutorial();
        archive.CloseCategory();
        daily_news.ResetDailyNews();
        yield return new WaitForSeconds(transition_duration);
        
        day_number.text = day.ToString();
        time_number.text = time.ToString() + ":00";
        
        transition_image.Play("EndTransition");
        AudioManager.instance.PlayMusic("Theme");
        AudioManager.instance.PlayHorrorMusic("Ambient");
        change_scene.ResetScene(); // Back to front desk
        daily_quota.UpdateDailyQuota(day);
        ScoreManager.instance.UpdateTargetScore(day);
        ScoreManager.instance.ResetScore();

        AP.severejumpscare_on = false;
        AP.hardjumpscare_on = false;

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
            Debug.Log("Daily Quota Reached : " + daily_quota.daily_fact + daily_quota.daily_catch);
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
