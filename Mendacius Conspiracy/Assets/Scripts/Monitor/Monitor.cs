using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Monitor : MonoBehaviour
{
    public GameObject fact_checking_rules;
    public GameObject catch_game_rules;
    public GameObject daily_news_box;
    public GameObject factual_image;
    private bool catch_rules_active = false;
    public bool fact_checking_rules_active = false;
    private bool daily_news_active = false;
    private bool factual_image_active = false;

    // References
    private ArticleManager article_manager;
    private DailyNewsManager daily_news;
    private DailyQuota daily_quota;
    public ActionPoint AP;
    public Text target_score_text;
    //public GameObject completed_text;
    private void Awake()
    {
        article_manager = GetComponent<ArticleManager>();
        daily_news = GetComponent<DailyNewsManager>();
        daily_quota = GetComponent<DailyQuota>();
    }

    private void Start()
    {
        fact_checking_rules.SetActive(false);
        catch_game_rules.SetActive(false);
        daily_news_box.SetActive(false);
        factual_image.SetActive(false);
        //completed_text.SetActive(false);
    }
    public void OpenCatchRules()
    {
        if (fact_checking_rules_active)
        {
            CloseFactCheckingGame();
        }
        if (daily_news_active)
        {
            CloseDailyNews();
        }
        if (factual_image_active)
        {
            CloseFactualImage();
        }
        if (catch_rules_active)
        {
            AudioManager.instance.PlaySFX("Click");
            CloseCatchRules();
        }
        else
        {
            AudioManager.instance.PlaySFX("Click");
            catch_game_rules.SetActive(true);
            catch_rules_active = true;
        }
    }
    public void CloseCatchRules()
    {
        catch_game_rules.SetActive(false);
        catch_rules_active = false;
    }
    public void OpenFactCheckingGame()
    {
        if (catch_rules_active)
        {
            CloseCatchRules();
        }
        if (daily_news_active)
        {
            CloseDailyNews();
        }
        if (factual_image_active)
        {
            CloseFactualImage();
        }
        if (fact_checking_rules_active)
        {
            AudioManager.instance.PlaySFX("Click");
            CloseFactCheckingGame();
        }
        else
        {
            if(AP.has_AP)
            {
                AudioManager.instance.PlaySFX("Click");
                fact_checking_rules.SetActive(true);
                fact_checking_rules_active = true;
                article_manager.PlayFactChecking();
            }
/*            else
            {
                fact_checking_rules_active = true;
                //completed_text.SetActive(true);
            }*/
        }
    }
    public void CloseFactCheckingGame()
    {
        fact_checking_rules.SetActive(false);
        fact_checking_rules_active = false;
        //completed_text.SetActive(false);
    }
    public void OpenFactualImage()
    {
        CloseCatchRules();
        CloseFactCheckingGame();
        CloseDailyNews();

        if (factual_image_active)
        {
            AudioManager.instance.PlaySFX("Click");
            CloseFactualImage();
        }
        else
        {
            AudioManager.instance.PlaySFX("Click");
            factual_image.SetActive(true);
            factual_image_active = true;
        }
    }
    public void CloseFactualImage()
    {
        factual_image.SetActive(false);
        factual_image_active = false;
    }
    public void OpenDailyNews()
    {
        CloseFactCheckingGame();
        CloseCatchRules();
        CloseFactualImage();
        if (daily_news_active)
        {
            AudioManager.instance.PlaySFX("Click");
            CloseDailyNews();
        }
        else
        {
            if (AP.has_AP)
            {
                AudioManager.instance.PlaySFX("Click");
                daily_news_box.SetActive(true);
                daily_news.OpenDailyNews();
                daily_news_active = true;
            }
        }
    }
    public void CloseDailyNews()
    {
        daily_news_box.SetActive(false);
        daily_news_active = false;
    }
    public void CloseAllMonitor()
    {
        CloseDailyNews();
        CloseCatchRules();
        CloseFactCheckingGame();
        CloseFactualImage();
    }
    private void Update()
    {
        target_score_text.text = ScoreManager.instance.target_score.ToString();
    }
}
