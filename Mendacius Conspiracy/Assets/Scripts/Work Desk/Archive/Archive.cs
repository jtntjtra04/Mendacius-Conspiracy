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
    private bool category_active = false;

    private void Start()
    {
        category_list.SetActive(false);
        category_dropdown.onValueChanged.AddListener(HandleCategory);
    }
    public void OpenCategory()
    {
        category_list.SetActive(true);
        HandleCategory(category_dropdown.value);
        category_active = true;
    }
    public void CloseCategory()
    {
        category_list.SetActive(false);
        health_paper.SetActive(false);
        food_paper.SetActive(false);
        lifestyle_paper.SetActive(false);
        category_active = false;
    }
    public void HandleCategory(int value)
    {
        if(value == 0)
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(category_active)
            {
                CloseCategory();
            }
        }
    }
}
