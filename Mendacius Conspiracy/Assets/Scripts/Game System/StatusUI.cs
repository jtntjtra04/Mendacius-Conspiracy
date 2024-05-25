using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private ActionPoint player_AP;
    [SerializeField] private Credibility player_credibility;
    [SerializeField] private Image curr_AP;
    [SerializeField] private Image curr_credibility;

    private void Start()
    {
        curr_AP.fillAmount = player_AP.action_point / 10;
        curr_credibility.fillAmount = player_credibility.credibility / 10;
    }
    private void Update()
    {
        curr_AP.fillAmount = player_AP.action_point / 10;
        curr_credibility.fillAmount = player_credibility.credibility / 10;
    }
}
