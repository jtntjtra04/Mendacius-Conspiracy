using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoint : MonoBehaviour
{
    public float action_point;
    public bool has_AP;

    // References
    private TimeSystem time_system;
    public PhoneCall phone_call;
    private void Awake()
    {
        time_system = GetComponent<TimeSystem>();
    }
    private void Start()
    {
        action_point = 8f;
        has_AP = true;
    }
    public void UseActionPoint()
    {
        action_point--;
        if (phone_call.on_call)
        {
            phone_call.ForceCloseCall();
        }
        // 15% chance to get a call from worker
        float chance_call = Random.value;
        if (chance_call <= 0.15f && time_system.time < 24)
        {
            Debug.Log("Phone Ringing");
            phone_call.PhoneRinging();
        }

        if(action_point <= 0)
        {
            has_AP = false;
        }
    }
    public void ResetActionPoint()
    {
        action_point = 8f;
        has_AP = true;
    }
}
