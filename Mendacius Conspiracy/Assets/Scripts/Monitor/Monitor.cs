using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    public GameObject fact_checking_rules;
    public GameObject catch_game_rules;
    private bool catch_rules_active = false;
    private bool fact_checking_rules_active = false;

    private void Start()
    {
        fact_checking_rules.SetActive(false);
        catch_game_rules.SetActive(false);
    }
    public void OpenCatchRules()
    {
        if (fact_checking_rules_active)
        {
            CloseFactCheckingRules();
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
    public void OpenFactCheckingRules()
    {
        if (catch_rules_active)
        {
            CloseCatchRules();
        }
        if (fact_checking_rules_active)
        {
            CloseFactCheckingRules();
        }
        else
        {
            fact_checking_rules.SetActive(true);
            fact_checking_rules_active = true;
        }
    }
    public void CloseFactCheckingRules()
    {
        fact_checking_rules.SetActive(false);
        fact_checking_rules_active = false;
    }
    public void OpenFactualImage()
    {
        CloseCatchRules();
        CloseFactCheckingRules();
    }
    public void OpenDailyNews()
    {
        CloseFactCheckingRules();
        CloseCatchRules();
    }
}
