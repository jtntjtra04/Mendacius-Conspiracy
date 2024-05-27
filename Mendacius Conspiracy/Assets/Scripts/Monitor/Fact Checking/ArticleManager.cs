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
    public Text title_text;
    public Text author_text;
    public Text content1_text;
    public Text content2_text;
    public Image article_image;

    // Reference 
    public ActionPoint action_point;
    public Credibility cred;
    public TimeSystem time_system;
    public PhoneCall phone_call;
    public DailyQuota daily_quota;
    private Monitor monitor;

    // Other 
    public bool on_article;
    private void Awake()
    {
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
            if(daily_quota.curr_fact < daily_quota.daily_fact && action_point.has_AP)
            {
                GenerateRandomArticle();
                Debug.Log("Generate random Article");
            }
        }
    }
    private void GenerateRandomArticle()
    {
        on_article = true;

        if(article_data.Count == 0)
        {
            Debug.Log("No articles available");
            return;
        }
        // Classification by type
        List<ArticleData> fact_article = article_data.FindAll(article => article.type == ArticleType.Fact);
        List<ArticleData> hoaxworker_article = article_data.FindAll(article => article.type == ArticleType.HoaxWorker);
        List<ArticleData> hoaxuniversal_article = article_data.FindAll(article => article.type == ArticleType.UniversalHoax);

        bool fact_type = Random.value <= 0.45f;

        if(fact_type)
        {
            curr_article = fact_article[Random.Range(0, fact_article.Count)];
        }
        else
        {
            bool hoaxworker_type = Random.value <= 0.6f;

            if(hoaxworker_type)
            {
                curr_article = hoaxworker_article[Random.Range(0, hoaxworker_article.Count)];
            }
            else
            {
                curr_article = hoaxuniversal_article[Random.Range(0, hoaxuniversal_article.Count)];
            }
        }
        DisplayArticle(curr_article);
    }
    private void DisplayArticle(ArticleData article_now)
    {
        title_text.text = article_now.title;
        author_text.text = "Penulis : " + article_now.author;
        content1_text.text = article_now.content1;
        content2_text.text = article_now.content2;
        article_image.sprite = article_now.image;
    }
    public void FactButton()
    {
        if(daily_quota.curr_fact < daily_quota.daily_fact && action_point.has_AP)
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
        if(daily_quota.curr_fact < daily_quota.daily_fact && action_point.has_AP)
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
        }
        else
        {
            // PopUp Message
            cred.MinusCredibility(1);
            Debug.Log("Wrong Decision : -1 Credibility");
        }

        daily_quota.curr_fact++;
        daily_quota.UpdateFactCheckText();

        if(daily_quota.curr_fact < daily_quota.daily_fact && action_point.has_AP)
        {
            NextArticle();
        }
        else
        {
            monitor.CloseFactCheckingGame();
            on_article = false;
        }
    }
    private void NextArticle()
    {
        GenerateRandomArticle();
    }
}
