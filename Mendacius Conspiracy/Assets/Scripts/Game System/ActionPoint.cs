using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoint : MonoBehaviour
{
    public float action_point;
    public bool has_AP;

    // Jumpscare Mechanic
    public List<GameObject> jumpscares;
    public string[] jumpscare_sfx;

    // References
    private TimeSystem time_system;
    public PhoneCall phone_call;
    private Credibility cred;
    public RandomCall random_call;
    private void Awake()
    {
        time_system = GetComponent<TimeSystem>();
        cred = GetComponent<Credibility>();
    }
    private void Start()
    {
        action_point = 8f;
        has_AP = true;
        foreach(GameObject jumpscare in jumpscares)
        {
            jumpscare.SetActive(false);
        }
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
            phone_call.PhoneRinging();
        }
        if(cred.credibility == 4)
        {
            float chance_jumpscare = Random.value;
            if (chance_jumpscare <= 0.1f)
            {
                StartCoroutine(JumpscareTrigger());
            }
        }
        else if(cred.credibility == 3)
        {
            float chance_jumpscare = Random.value;
            if(chance_jumpscare <= 0.15f)
            {
                StartCoroutine(JumpscareTrigger());
            }
        }
        else if (cred.credibility == 2)
        {
            float chance_jumpscare = Random.value;
            if (chance_jumpscare <= 0.2f)
            {
                StartCoroutine(JumpscareTrigger());
            }
        }
        else if (cred.credibility == 1)
        {
            float chance_jumpscare = Random.value;
            if (chance_jumpscare <= 0.25f)
            {
                StartCoroutine(JumpscareTrigger());
            }
        }

        if (action_point <= 0)
        {
            has_AP = false;
        }
    }
    public void ResetActionPoint()
    {
        action_point = 8f;
        has_AP = true;
    }
    private IEnumerator JumpscareTrigger()
    {
        int jumpscare_index = Random.Range(0, jumpscares.Count);
        jumpscares[jumpscare_index].SetActive(true);

        int sfx_index = Random.Range(0, jumpscare_sfx.Length);
        AudioManager.instance.PlaySFX(jumpscare_sfx[sfx_index]);
        AudioManager.instance.music_source.Stop();

        yield return new WaitForSeconds(30f);
        if(cred.credibility <= 3)
        {
            float chance_call = Random.value;
            if(chance_call <= 0.3f)
            {
                random_call.CallRandomWorker();
            }
        }
        yield return new WaitForSeconds(60f);
        jumpscares[jumpscare_index].SetActive(true);
        AudioManager.instance.PlayMusic("Theme");
    }
}
