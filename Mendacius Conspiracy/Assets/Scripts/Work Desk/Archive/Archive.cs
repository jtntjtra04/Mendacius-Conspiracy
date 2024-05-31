using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archive : MonoBehaviour
{
    public GameObject category_list;
    public GameObject health_paper;
    public GameObject food_paper;
    public GameObject lifestyle_paper;
    public Dropdown category_dropdown;

    private void Start()
    {
        category_list.SetActive(false);
        category_dropdown.onValueChanged.AddListener(HandleCategory);
    }
    public void OpenCategory()
    {
        AudioManager.instance.PlaySFX("Click");
        category_list.SetActive(true);
        HandleCategory(category_dropdown.value);
    }
    public void CloseCategory()
    {
        AudioManager.instance.PlaySFX("Click");
        category_list.SetActive(false);
        health_paper.SetActive(false);
        food_paper.SetActive(false);
        lifestyle_paper.SetActive(false);
    }
    public void HandleCategory(int value)
    {
        AudioManager.instance.PlaySFX("Click");
        if (value == 0)
        {
            health_paper.SetActive(true);
            food_paper.SetActive(false);
            lifestyle_paper.SetActive(false);
            Debug.Log("Health Paper Active");
        }
        if(value == 1)
        {
            health_paper.SetActive(false);
            food_paper.SetActive(true);
            lifestyle_paper.SetActive(false);
        }
        if (value == 2)
        {
            health_paper.SetActive(false);
            food_paper.SetActive(false);
            lifestyle_paper.SetActive(true);
        }
    }
}
