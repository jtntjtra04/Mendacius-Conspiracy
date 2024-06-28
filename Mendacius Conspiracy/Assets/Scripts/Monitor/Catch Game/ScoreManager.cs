using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // For instance

    // UI Text
    public Text score_text;
    public Text fact_text;
    public Text hoax_text;
    public Text superfact_text;
    public Text fatalhoax_text;

    // Initial Count
    private int fact_count = 0;
    private int hoax_count = 0;
    private int superfact_count = 0;
    private int fatalhoax_count = 0;
    public int score = 0;
    public int target_score;

    private void Awake()
    {
        /*if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
        instance = this;

    }
    private void Start()
    {
        score_text.text = score.ToString();
        fact_text.text = fact_count.ToString();
        hoax_text.text = hoax_count.ToString();
        superfact_text.text = superfact_count.ToString();
        fatalhoax_text.text = fatalhoax_count.ToString();
        target_score = 200;
    }
    public void AddScore(int point)
    {
        score += point;
        score_text.text = score.ToString();
    }
    public void SubstractScore(int point)
    {
        if(score > 0)
        {
            score -= point;
            score_text.text = score.ToString();
        }
    }
    public void AddFactCount(int count)
    {
        fact_count += count;
        fact_text.text = fact_count.ToString();
    }
    public void AddHoaxCount(int count)
    {
        hoax_count += count;
        hoax_text.text = hoax_count.ToString();
    }
    public void AddSuperFactCount(int count)
    {
        superfact_count += count;
        superfact_text.text = superfact_count.ToString();
    }
    public void AddFatalHoaxCount(int count)
    {
        fatalhoax_count += count;
        fatalhoax_text.text = fatalhoax_count.ToString();
    }
    public void UpdateTargetScore(int day)
    {
        if (day == 2)
        {
            target_score = 200; // Target score Daily catch game
        }
        else if (day >= 3 && day <= 4)
        {
            target_score = 300;
        }
        else if (day >= 5 && day <= 6)
        {
            target_score = 400;
        }
        else if (day == 7)
        {
            target_score = 450;
        }
    }
    public void ResetScore()
    {
        score = 0;
        fact_count = 0;
        superfact_count = 0;
        hoax_count = 0;
        fatalhoax_count = 0;

        score_text.text = score.ToString();
        fact_text.text = fact_count.ToString();
        hoax_text.text = hoax_count.ToString();
        superfact_text.text = superfact_count.ToString();
        fatalhoax_text.text = fatalhoax_count.ToString();
    }

}
