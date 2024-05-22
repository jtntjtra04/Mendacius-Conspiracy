using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigationBoard : MonoBehaviour
{
    public GameObject investigation_board;
    public static bool board_active = false;

    // References
    public GameObject wd_button;

    private void Start()
    {
        investigation_board.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (board_active)
            {
                CloseBoard();
            }
        }
    }
    public void OpenBoard()
    {
        investigation_board.SetActive(true);
        wd_button.SetActive(false);
        board_active = true;
    }
    public void CloseBoard()
    {
        investigation_board.SetActive(false);
        wd_button.SetActive(true);
        board_active = false;
    }
}
