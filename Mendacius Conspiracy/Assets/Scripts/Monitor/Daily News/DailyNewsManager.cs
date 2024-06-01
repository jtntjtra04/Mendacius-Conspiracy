using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyNewsManager : MonoBehaviour
{
    public NewsData news_data;
    public Text title_text;
    public Text content_text;

    private int curr_day;
    private bool today_news_accessed;

    // References
    public TimeSystem time_system;
    public ActionPoint action_point;
    //public RandomCall random_call;
    public PhoneCall phone_call;

    private void Start()
    {
        curr_day = time_system.day;
        today_news_accessed = false;
    }
    public void OpenDailyNews()
    {
        if(!today_news_accessed && action_point.has_AP)
        {
            action_point.UseActionPoint();
            today_news_accessed = true;
            UpdateDailyNews();
        }
        else
        {
            Debug.Log("Already accessed news today, not using Action Point to access again");
        }
    }
    private void UpdateDailyNews()
    {
        curr_day = time_system.day;
        //int variation = GetDailyNewsVariation();
        var daily_news = news_data.GetDailyNews(curr_day);
        title_text.text = daily_news.title;
        content_text.text = daily_news.content;
        Debug.Log("Daily news Update");
        if (!phone_call.on_call)
        {
            time_system.UpdateTime();
        }
    }
/*    private int GetDailyNewsVariation()
    {
        int troll_worker = random_call.troll_index; // get a troll worker index

        if(troll_worker == 0 || troll_worker == 2)
        {
            return 1;
        }
        else if(troll_worker == 1 || troll_worker == 3)
        {
            return 2;
        }
        else
        {
            return 1; // default when no worker are found
        }
    }*/
    public void ResetDailyNews()
    {
        today_news_accessed = false;
    }
}
