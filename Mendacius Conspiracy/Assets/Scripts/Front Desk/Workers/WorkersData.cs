using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersData : MonoBehaviour
{
    public GameObject workers_data;
    public GameObject[] workers_paper;
    private bool WD_active = false;
    private int curr_index = 0;

    private void Start()
    {
        workers_data.SetActive(false);
        workers_paper[0].SetActive(true); // when called always index 0 show first
        for(int i = 1; i < workers_paper.Length; i++) // to ensure the rest of workers paper inactive if workers data are called
        {
            workers_paper[i].SetActive(false);
        }
    }
    public void OpenWorkersData()
    {
        AudioManager.instance.PlaySFX("Click");
        workers_data.SetActive(true);
        curr_index = 0;
        workers_paper[curr_index].SetActive(true);
        WD_active = true;
    }
    public void CloseWorkersData()
    {
        AudioManager.instance.PlaySFX("Click");
        workers_data.SetActive(false);
        workers_paper[curr_index].SetActive(false);
        WD_active = false;
    }
    public void RightShift()
    {
        workers_paper[curr_index].SetActive(false); // Deactivate the current Worker Paper
        curr_index++; // Increment index / shift to right
        if (curr_index >= workers_paper.Length)
        {
            curr_index = 0;
        }
        workers_paper[curr_index].SetActive(true); // Activate the next worker paper
        AudioManager.instance.PlaySFX("FlipPage");
    }
    public void LeftShift()
    {
        workers_paper[curr_index].SetActive(false); // Deactivate the current Worker Paper
        curr_index--; // Decrement index / shift to left
        if(curr_index < 0)
        {
            curr_index = workers_paper.Length - 1;
        }
        workers_paper[curr_index].SetActive(true); // Activate the previous worker paper
        AudioManager.instance.PlaySFX("FlipPage");
    }
}
