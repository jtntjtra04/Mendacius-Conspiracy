using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArticleManager : MonoBehaviour
{
    // Article Database
    public List<ArticleData> article_data;
    private ArticleData curr_article;

    // UI
    public Text author_text;
    public Image article_image;

    // Reference 
    public ActionPoint action_point;
    public Credibility cred;
    public TimeSystem time_system;
    public PhoneCall phone_call;
    private DailyQuota daily_quota;
    private Monitor monitor;
    public RandomCall random_call;

    // Other 
    public bool on_article;
    private void Awake()
    {
        daily_quota = GetComponent<DailyQuota>();
        monitor = GetComponent<Monitor>();
    }

    private void Start()
    {
        on_article = false;
        //article_data = new List<ArticleData>(Resources.LoadAll<ArticleData>("Articles"));
    }
    public void PlayFactChecking()
    {
        if (!on_article)
        {
            if(action_point.has_AP && cred.credibility > 0)
            {
                GenerateRandomArticle();
                Debug.Log("Generate random Article");
            }
        }
    }
    DayRange GetCurrentDayRange() // To Search what is the current day to return the day range
    {
        int curr_day = time_system.day;
        if(curr_day == 1 ||  curr_day== 2)
        {
            return DayRange.Range1;
        }
        else if(curr_day == 3 || curr_day == 4)
        {
            return DayRange.Range2;
        }
        else
        {
            return DayRange.Range3;
        }
    }
    private void GenerateRandomArticle()
    {
        on_article = true;

        if(article_data.Count == 0)
        {
            Debug.Log("No articles available from database");
            return;
        }

        DayRange curr_range = GetCurrentDayRange(); // Get current Day Range from day

        // Classification by range day
        List<ArticleData> day_article = article_data.FindAll(article => article.day_range == curr_range);

        if(day_article.Count == 0)
        {
            Debug.Log("No articles available for the current day range");
            return;
        }


        bool fact_type = Random.value <= 0.45f;

        if(fact_type)
        {
            List<ArticleData> fact_article = day_article.FindAll(article => article.type == ArticleType.Fact);
            if(fact_article.Count > 0)
            {
                curr_article = fact_article[Random.Range(0, fact_article.Count)];
            }
        }
        else
        {
            bool hoaxworker_type = Random.value <= 0.6f;

            if(hoaxworker_type && curr_range != DayRange.Range1)
            {
                AlienWorker curr_alien = (AlienWorker)random_call.troll_index;
                List<ArticleData> specified_hoaxworker = day_article.FindAll(article => article.type == ArticleType.HoaxWorker && article.alien_worker == curr_alien);

                if(specified_hoaxworker.Count > 0)
                {
                    curr_article = specified_hoaxworker[Random.Range(0, specified_hoaxworker.Count)];
                    Debug.Log("Display the article from : " + curr_alien);
                }
                else
                {
                    List<ArticleData> hoaxuniversal_article = day_article.FindAll(article => article.type == ArticleType.UniversalHoax);
                    if(hoaxuniversal_article.Count > 0)
                    {
                        curr_article = hoaxuniversal_article[Random.Range(0, hoaxuniversal_article.Count)];
                    }
                    Debug.Log("Not found specified hoax worker");
                }
            }
            else
            {
                List<ArticleData> hoaxuniversal_article = day_article.FindAll(article => article.type == ArticleType.UniversalHoax);
                if(hoaxuniversal_article.Count > 0)
                {
                    curr_article = hoaxuniversal_article[Random.Range(0, hoaxuniversal_article.Count)];
                }
            }
        }
        DisplayArticle(curr_article);
    }
    private void DisplayArticle(ArticleData article_now)
    {
        author_text.text = "Penulis : " + article_now.author;
        article_image.sprite = article_now.image;
    }
    public void FactButton()
    {
        AudioManager.instance.PlaySFX("ClickSpace");
        if(action_point.has_AP && cred.credibility > 0)
        {
            action_point.UseActionPoint(); // use action point

            if (curr_article.type == ArticleType.Fact)
            {
                HandleDecision(true);
            }
            else
            {
                HandleDecision(false);
            }
            // Update Time when no call
            if (!phone_call.on_call)
            {
                time_system.UpdateTime();
            }
        }
    }
    public void HoaxButton()
    {
        AudioManager.instance.PlaySFX("ClickSpace");
        if (action_point.has_AP && cred.credibility > 0)
        {
            action_point.UseActionPoint(); // use action point

            if (curr_article.type != ArticleType.Fact)
            {
                HandleDecision(true);
            }
            else
            {
                HandleDecision(false);
            }
            // Update Time when no call
            if (!phone_call.on_call)
            {
                time_system.UpdateTime();
            }
        }
    }
    private void HandleDecision(bool decision)
    {
        if(decision)
        {
            // PopUp Message
            Debug.Log("Correct Decision");
            AudioManager.instance.PlaySFX("Correct");
            if (daily_quota.curr_fact < daily_quota.daily_fact)
            {
                daily_quota.curr_fact++;
                daily_quota.UpdateFactCheckText();
            }
        }
        else
        {
            // PopUp Message
            cred.MinusCredibility(1);
            Debug.Log("Wrong Decision : -1 Credibility");
            Notification.Instance.AddQueue("-1 Credibility");
            AudioManager.instance.PlaySFX("Wrong");
        }

        if(action_point.has_AP)
        {
            NextArticle();
        }
        else
        {
            monitor.CloseFactCheckingGame();
            monitor.fact_checking_rules_active = true;
            //monitor.completed_text.SetActive(true);
            on_article = false;
        }
    }
    private void NextArticle()
    {
        GenerateRandomArticle();
    }
}
