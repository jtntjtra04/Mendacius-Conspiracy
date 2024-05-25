using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoint : MonoBehaviour
{
    public int action_point;
    public bool has_AP;
    private void Start()
    {
        action_point = 8;
        has_AP = true;
    }
    public void UseActionPoint()
    {
        action_point--;

        // 15% chance to get a call from worker
        float chance_call = Random.value;
        if (chance_call <= 0.15f)
        {
            // ring
        }

        if(action_point <= 0)
        {
            has_AP = false;
        }
    }
    public void ResetActionPoint()
    {
        action_point = 8;
    }
}
