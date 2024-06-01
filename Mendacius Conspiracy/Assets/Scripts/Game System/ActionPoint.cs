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
    public GameObject[] hard_jumpscares;
    public bool severejumpscare_on;
    public bool hardjumpscare_on;

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
        severejumpscare_on = false;
        hardjumpscare_on = false;
        has_AP = true;
        foreach(GameObject jumpscare in jumpscares)
        {
            jumpscare.SetActive(false);
        }
        foreach(GameObject hard_jumpscare in hard_jumpscares)
        {
            hard_jumpscare.SetActive(false);
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
            else
            {
                chance_jumpscare = Random.value;
                if(chance_jumpscare <= 0.2f && !severejumpscare_on)
                {
                    StartCoroutine(SevereJumpscare());
                }
            }
        }
        else if (cred.credibility == 1)
        {
            float chance_jumpscare = Random.value;
            if (chance_jumpscare <= 0.25f)
            {
                StartCoroutine(JumpscareTrigger());
            }
            else
            {
                chance_jumpscare = Random.value;
                if (chance_jumpscare <= 0.15f && !severejumpscare_on)
                {
                    StartCoroutine(HardJumpscare());
                }
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
        jumpscares[jumpscare_index].SetActive(false);
        AudioManager.instance.PlayMusic("Theme");
    }
    private IEnumerator SevereJumpscare()
    {
        // turn on jumpscare
        severejumpscare_on = true;
        AudioManager.instance.music_source.Stop();
        AudioManager.instance.horror_source.Stop();
        yield return new WaitForSeconds(7f);
        hard_jumpscares[0].SetActive(true);
        AudioManager.instance.PlaySFX("BrokenRadio");
        
        yield return new WaitForSeconds(1.5f);

        // turn off jumpscare
        hard_jumpscares[0].SetActive(false);
        AudioManager.instance.sfx_source.Stop();
        yield return new WaitForSeconds(5f);
        AudioManager.instance.PlayHorrorMusic("Ambient");
        yield return new WaitForSeconds(20f);
        AudioManager.instance.PlayMusic("Theme");
        severejumpscare_on = false;
    }
    private IEnumerator HardJumpscare()
    {
        // turn on jumpscare
        hardjumpscare_on = true;
        AudioManager.instance.music_source.Stop();
        AudioManager.instance.horror_source.Stop();
        yield return new WaitForSeconds(7f);
        hard_jumpscares[1].SetActive(true);
        AudioManager.instance.PlaySFX("Scream");
        yield return new WaitForSeconds(3f);

        // turn off jumpscare
        hard_jumpscares[1].SetActive(false);
        AudioManager.instance.sfx_source.Stop();
        yield return new WaitForSeconds(10f);
        AudioManager.instance.PlayHorrorMusic("Ambient");
        yield return new WaitForSeconds(30f);
        AudioManager.instance.PlayMusic("Theme");
        hardjumpscare_on = false;
    }
    public void HardJumpscareTrigger()
    {
        StartCoroutine(HardJumpscare());
    }
}
