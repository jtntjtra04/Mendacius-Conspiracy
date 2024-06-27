using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoint : MonoBehaviour, IDataManager
{
    public float action_point;
    public bool has_AP;

    // Jumpscare Mechanic
    public List<GameObject> jumpscares;
    public string[] jumpscare_sfx;
    public GameObject[] hard_jumpscares;
    public bool severejumpscare_on;
    public bool hardjumpscare_on;

    // Dark Mechanic
    private int darkmode_active = 0;
    public GameObject front_lightning;
    public GameObject front_darklightning;
    public GameObject work_lightning;
    public GameObject work_darklightning;
    public GameObject work_PC_light;
    public GameObject work_PC_dark;

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
        //action_point = 8f;
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
        // Lightning
        front_lightning.SetActive(true);
        front_darklightning.SetActive(false);
        work_lightning.SetActive(true);
        work_darklightning.SetActive(false);
        work_PC_light.SetActive(true);
        work_PC_dark.SetActive(false);
    }
    public void LoadData(GameData data)
    {
        this.action_point = data.action_point;
    }
    public void SaveData(GameData data)
    {
        data.action_point = this.action_point;
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
        if (chance_call <= 0.15f && time_system.time < 24 && !phone_call.on_tutorial)
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
            if (chance_jumpscare <= 0.15f && !severejumpscare_on)
            {
                StartCoroutine(SevereJumpscare());
            }
            else
            {
                float secondchance_jumpscare = Random.value;
                if (secondchance_jumpscare <= 0.2f)
                {
                    StartCoroutine(JumpscareTrigger());
                }
            }
        }
        else if (cred.credibility == 1)
        {
            float chance_jumpscare = Random.value;
            if (chance_jumpscare <= 0.15f && !hardjumpscare_on)
            {
                StartCoroutine(ExpansionJumpscare());
            }
            else
            {
                float secondchance_jumpscare = Random.value;
                if (secondchance_jumpscare <= 0.25f)
                {
                    StartCoroutine(JumpscareTrigger());
                }
                else
                {
                    float thirdchance_jumpscare = Random.value;
                    if(thirdchance_jumpscare <= 0.2f && !severejumpscare_on)
                    {
                        StartCoroutine(SevereJumpscare());
                    }
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

        if(jumpscare_index == 5 || jumpscare_index == 4)
        {
            DarkMode();
        }
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
        if (jumpscare_index == 5 || jumpscare_index == 4)
        {
            darkmode_active--;
            if(darkmode_active == 0)
            {
                LightMode();
            }
        }
        AudioManager.instance.PlayMusic("Theme");
    }
    private IEnumerator SevereJumpscare()
    {
        // turn on jumpscare
        severejumpscare_on = true;
        hard_jumpscares[0].SetActive(true);
        AudioManager.instance.PlaySFX("Piano");
        
        yield return new WaitForSeconds(1f);
        AudioManager.instance.music_source.Stop();
        AudioManager.instance.horror_source.Stop();

        // turn off jumpscare
        hard_jumpscares[0].SetActive(false);
        AudioManager.instance.sfx_source.Stop();
        yield return new WaitForSeconds(15f);
        AudioManager.instance.PlayHorrorMusic("Ambient");
        yield return new WaitForSeconds(300f);
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
        yield return new WaitForSeconds(15f);
        AudioManager.instance.PlayHorrorMusic("Ambient");
        yield return new WaitForSeconds(300f);
        AudioManager.instance.PlayMusic("Theme");
        hardjumpscare_on = false;
    }
    private IEnumerator ExpansionJumpscare()
    {
        // turn on jumpscare
        hardjumpscare_on = true;
        AudioManager.instance.music_source.Stop();
        AudioManager.instance.horror_source.Stop();
        yield return new WaitForSeconds(7f);
        hard_jumpscares[2].SetActive(true);
        AudioManager.instance.PlaySFX("Tension");
        yield return new WaitForSeconds(7f);

        // turn off jumpscare
        hard_jumpscares[2].SetActive(false);
        yield return new WaitForSeconds(15f);
        AudioManager.instance.PlayHorrorMusic("Ambient");
        yield return new WaitForSeconds(300f);
        AudioManager.instance.PlayMusic("Theme");
        hardjumpscare_on = false;
    }
    public void HardJumpscareTrigger()
    {
        StartCoroutine(HardJumpscare());
    }
    private void DarkMode()
    {
        if(darkmode_active == 0)
        {
            AudioManager.instance.PlaySFX("LightsOff");
        }
        darkmode_active++;
        front_lightning.SetActive(false);
        front_darklightning.SetActive(true);
        work_lightning.SetActive(false);
        work_darklightning.SetActive(true);
        work_PC_light.SetActive(false);
        work_PC_dark.SetActive(true);
    }
    public void LightMode()
    {
        AudioManager.instance.PlaySFX("LightsOn");
        darkmode_active = 0;
        front_lightning.SetActive(true);
        front_darklightning.SetActive(false);
        work_lightning.SetActive(true);
        work_darklightning.SetActive(false);
        work_PC_light.SetActive(true);
        work_PC_dark.SetActive(false);
    }
}
