using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monitor : MonoBehaviour
{
    public GameObject fact_checking_rules;
    public GameObject catch_game_rules;
    private bool catch_rules_active = false;
    private bool fact_checking_rules_active = false;

    // References
    private ArticleManager article_manager;
    public DailyQuota daily_quota;
    public ActionPoint AP;
    public Text target_score_text;
    private void Awake()
    {
        article_manager = GetComponent<ArticleManager>();
    }

    private void Start()
    {
        fact_checking_rules.SetActive(false);
        catch_game_rules.SetActive(false);
    }
    public void OpenCatchRules()
    {
        if (fact_checking_rules_active)
        {
            CloseFactCheckingGame();
        }

        if (catch_rules_active)
        {
            CloseCatchRules();
        }
        else
        {
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
        if (fact_checking_rules_active)
        {
            CloseFactCheckingGame();
        }
        else
        {
            if(daily_quota.curr_fact < daily_quota.daily_fact && AP.has_AP)
            {
                fact_checking_rules.SetActive(true);
                fact_checking_rules_active = true;
                article_manager.PlayFactChecking();
            }
        }
    }
    public void CloseFactCheckingGame()
    {
        fact_checking_rules.SetActive(false);
        fact_checking_rules_active = false;
    }
    public void OpenFactualImage()
    {
        CloseCatchRules();
        CloseFactCheckingGame();
    }
    public void OpenDailyNews()
    {
        CloseFactCheckingGame();
        CloseCatchRules();
    }
    private void Update()
    {
        target_score_text.text = ScoreManager.instance.target_score.ToString();
    }
}
