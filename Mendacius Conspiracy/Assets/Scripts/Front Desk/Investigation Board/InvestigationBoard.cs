using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigationBoard : MonoBehaviour
{
    public GameObject investigation_board;

    // References
    public GameObject wd_button;
    private void Start()
    {
        investigation_board.SetActive(false);
    }
    public void OpenBoard()
    {
        AudioManager.instance.PlaySFX("Click");
        investigation_board.SetActive(true);
        wd_button.SetActive(false);
    }
    public void CloseBoard()
    {
        AudioManager.instance.PlaySFX("Click");
        investigation_board.SetActive(false);
        wd_button.SetActive(true);
    }
}
