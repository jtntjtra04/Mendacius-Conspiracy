using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyQuota : MonoBehaviour
{
    public int daily_fact;
    public int daily_catch;
    public int curr_fact;
    public int curr_catch;

    // UI
    public Text factcheck_text;
    public Text infocatch_text;
    public void UpdateDailyQuota(int day)
    {
        curr_fact = 0;
        curr_catch = 0;

        if(day == 1)
        {
            daily_fact = 1;
            daily_catch = 0;
        }
        else if (day == 2)
        {
            daily_fact = 1;
            daily_catch = 1;
        }
        else if(day >= 3 && day <= 5)
        {
            daily_fact = 2;
            daily_catch = 1;
        }
        else if(day >= 6 && day <= 7)
        {
            daily_fact = 3;
            daily_catch = 1;
        }
        UpdateFactCheckText();
        UpdateInfoCatchText();
        Debug.Log("Day : " + day);
    }
    public void UpdateFactCheckText()
    {
        factcheck_text.text = "Fact Check " + curr_fact + "/" + daily_fact;
    }
    public void UpdateInfoCatchText()
    {
        infocatch_text.text = "Info Catcher " + curr_catch + "/" + daily_catch;
    }
    
}
