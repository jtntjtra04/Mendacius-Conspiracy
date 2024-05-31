using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCall : MonoBehaviour
{
    public GameObject phone_dialoguebox;
    public Text message_text;
    private Coroutine time_out_coroutine;
    public bool on_call = false;

    [System.Serializable]
    public class WorkingHoursData
    {
        public string name;
        public int start_hour_1;
        public int end_hour_1;
        public int start_hour_2;
        public int end_hour_2;
    }

    // List Worker's Working hours
    public List<WorkingHoursData> working_hours = new List<WorkingHoursData>();

    // Worker Messages
    private Dictionary<string, string> messages = new Dictionary<string, string>()
    {
        { "Anna", "Halo, ini Anna dari departemen editorial. Saya membutuhkan akses ke data perusahaan untuk pembuatan sebuah artikel. Bisa tolong diberi izin?" },
        { "Haris", "Halo, ini Haris dari departemen editorial. Saya membutuhkan akses ke data perusahaan untuk pembuatan sebuah artikel. Bisa tolong diberi izin?" },
        { "Lucia", "Halo, ini Lucia dari departemen editorial. Saya membutuhkan akses ke data perusahaan untuk pembuatan sebuah artikel. Bisa tolong diberi izin?" },
        { "Desmond", "Halo, ini Desmond dari departemen editorial. Saya membutuhkan akses ke data perusahaan untuk pembuatan sebuah artikel. Bisa tolong diberi izin?" }
    };

    private WorkingHoursData curr_worker;
    private bool is_troll;
    private int call_hour;

    // References
    public TimeSystem time_system;
    public Credibility player_credibility;
    public Animator notif_anim;
    private void Start()
    {
        phone_dialoguebox.SetActive(false);
        on_call = false;
    }
    public void PhoneRinging()
    {
        float chance_trollcall = Random.value;
        if(chance_trollcall <= 0.5f)
        {
            HandleCall(true);
        }
        else
        {
            HandleCall(false);
        }
    }
    private void HandleCall(bool is_troll)
    {
        on_call = true;
        this.is_troll = is_troll;

        int worker_index = Random.Range(0, working_hours.Count); // Select Worker Randomly
        WorkingHoursData worker = working_hours[worker_index];

        curr_worker = worker;
        call_hour = time_system.time;
        
        if(is_troll)
        {
            WorkingHoursData fake_worker = working_hours[Random.Range(0, working_hours.Count)];
            curr_worker = fake_worker;
        }
        string message = messages[curr_worker.name];
        DisplayDialogue(message);

        // Start time out 
        time_out_coroutine = StartCoroutine(TimeOut());
    }
    private void DisplayDialogue(string message)
    {
        phone_dialoguebox.SetActive(true);
        message_text.text = message;
    }
    public void AcceptCall()
    {
        StopCoroutine(time_out_coroutine);
        AudioManager.instance.hybrid_source.Stop();
        AudioManager.instance.hybrid_source.loop = false;
        AudioManager.instance.PlaySFX("PickUp");

        if ((call_hour >= curr_worker.start_hour_1 && call_hour <= curr_worker.end_hour_1) || (call_hour >= curr_worker.start_hour_2 && call_hour <= curr_worker.end_hour_2))
        {
            Debug.Log("Accepted safely");
            Notification.Instance.AddQueue("Call Accepted");
        }
        else
        {
            Debug.Log("Wrong Decision, -1 credibility");
            player_credibility.MinusCredibility(1);
            Notification.Instance.AddQueue("-1 Credibility");
        }
        phone_dialoguebox.SetActive(false);
        on_call = false;
        time_system.UpdateTime();
    }
    public void DenyCall()
    {
        StopCoroutine(time_out_coroutine);
        AudioManager.instance.hybrid_source.Stop();
        AudioManager.instance.hybrid_source.loop = false;
        AudioManager.instance.PlaySFX("PickUp");

        if ((call_hour >= curr_worker.start_hour_1 && call_hour <= curr_worker.end_hour_1) || (call_hour >= curr_worker.start_hour_2 && call_hour <= curr_worker.end_hour_2))
        {
            Debug.Log("Wrong Decision, -1 credibility");
            player_credibility.MinusCredibility(1);
            Notification.Instance.AddQueue("-1 Credibility");
        }
        else
        {
            Debug.Log("Accepted safely");
            Notification.Instance.AddQueue("Call Accepted");
        }
        phone_dialoguebox.SetActive(false);
        on_call = false;
        time_system.UpdateTime();
    }
    private IEnumerator TimeOut()
    {
        AudioManager.instance.hybrid_source.loop = true;
        AudioManager.instance.PlayHybrid("Ringtone");
        yield return new WaitForSeconds(60f); // timer of the call
        AudioManager.instance.hybrid_source.Stop();
        AudioManager.instance.hybrid_source.loop = false;

        player_credibility.MinusCredibility(1); // - Credibility if player doesn't hang up the call in 1 minute
        Debug.Log("Penalty : -1 Credibility");
        Notification.Instance.AddQueue("Turned Off Call Automatically");

        phone_dialoguebox.SetActive(false);
        on_call = false;
        time_system.UpdateTime();
    }
    public void ForceCloseCall()
    {
        StopCoroutine(time_out_coroutine);
        AudioManager.instance.hybrid_source.Stop();
        AudioManager.instance.hybrid_source.loop = false;

        player_credibility.MinusCredibility(1);
        Debug.Log("Penalty : -1 Credibility");
        Notification.Instance.AddQueue("Turned Off Call Automatically");
        phone_dialoguebox.SetActive(false);
        on_call = false;
        time_system.UpdateTime();
    }
}
