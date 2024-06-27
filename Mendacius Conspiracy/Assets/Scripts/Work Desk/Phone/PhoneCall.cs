using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCall : MonoBehaviour, IDataManager
{
    public GameObject phone_dialoguebox;
    public Text message_text;
    private Coroutine time_out_coroutine;
    public bool on_call = false;

    // Tutorial
    public bool on_tutorial = false;
    public Coroutine tutorial_coroutine;
    private bool new_game = false;

    // References
    public DialogueUI dialogue_ui;

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
        { "Desmond", "Halo, ini Desmond dari departemen editorial. Saya membutuhkan akses ke data perusahaan untuk pembuatan sebuah artikel. Bisa tolong diberi izin?" },
        { "CEO", "Panggilan masuk dari CEO Mata Elang" }
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
        if(new_game)
        {
            TutorialRinging();
            new_game = false;
        }
    }
    public void LoadData(GameData data)
    {
        this.new_game = data.new_game;
    }
    public void SaveData(GameData data)
    {
        data.new_game = this.new_game;
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
        if (on_tutorial)
        {
            StopCoroutine(tutorial_coroutine);
            AudioManager.instance.hybrid_source.Stop();
            AudioManager.instance.hybrid_source.loop = false;
            AudioManager.instance.PlaySFX("PickUp");
            phone_dialoguebox.SetActive(false);
            tutorial_coroutine = StartCoroutine(TutorialYapping());
            return;
        }
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
            AudioManager.instance.PlaySFX("Buzzer");
        }
        phone_dialoguebox.SetActive(false);
        if (!dialogue_ui.on_dialogue)
        {
            time_system.UpdateTime();
        }
        on_call = false;
    }
    public void DenyCall()
    {
        if (on_tutorial)
        {
            StopCoroutine(tutorial_coroutine);
            AudioManager.instance.hybrid_source.Stop();
            AudioManager.instance.hybrid_source.loop = false;
            AudioManager.instance.PlaySFX("PickUp");
            phone_dialoguebox.SetActive(false);
            on_tutorial = false;
            return;
        }
        StopCoroutine(time_out_coroutine);
        AudioManager.instance.hybrid_source.Stop();
        AudioManager.instance.hybrid_source.loop = false;
        AudioManager.instance.PlaySFX("PickUp");

        if ((call_hour >= curr_worker.start_hour_1 && call_hour <= curr_worker.end_hour_1) || (call_hour >= curr_worker.start_hour_2 && call_hour <= curr_worker.end_hour_2))
        {
            Debug.Log("Wrong Decision, -1 credibility");
            player_credibility.MinusCredibility(1);
            Notification.Instance.AddQueue("-1 Credibility");
            AudioManager.instance.PlaySFX("Buzzer");
        }
        else
        {
            Debug.Log("Denied safely");
            Notification.Instance.AddQueue("Call Denied");
        }
        phone_dialoguebox.SetActive(false);
        if (!dialogue_ui.on_dialogue)
        {
            time_system.UpdateTime();
        }
        on_call = false;
    }
    private IEnumerator TimeOut()
    {
        AudioManager.instance.hybrid_source.loop = true;
        AudioManager.instance.PlayHybrid("Ringtone");
        yield return new WaitForSeconds(60f); // timer of the call
        AudioManager.instance.hybrid_source.Stop();
        AudioManager.instance.hybrid_source.loop = false;
        AudioManager.instance.PlaySFX("PickUp");

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
        AudioManager.instance.PlaySFX("PickUp");

        player_credibility.MinusCredibility(1);
        Debug.Log("Penalty : -1 Credibility");
        Notification.Instance.AddQueue("Turned Off Call Automatically");
        phone_dialoguebox.SetActive(false);
        on_call = false;
        time_system.UpdateTime();
    }
    private void TutorialRinging()
    {
        on_tutorial = true;
        string tutorial_message = messages["CEO"];
        DisplayDialogue(tutorial_message);
        tutorial_coroutine = StartCoroutine(TutorialCall());
    }
    private IEnumerator TutorialCall()
    {
        AudioManager.instance.hybrid_source.loop = true;
        AudioManager.instance.PlayHybrid("Ringtone");
        yield return new WaitForSeconds(60f); // timer of the call
        AudioManager.instance.hybrid_source.Stop();
        AudioManager.instance.hybrid_source.loop = false;
    }
    private IEnumerator TutorialYapping()
    {
        AudioManager.instance.music_source.volume = 0.2f;
        AudioManager.instance.PlayYapping("CEOYapping");
        yield return new WaitForSeconds(276f);
        if(player_credibility.credibility == 5)
        {
            AudioManager.instance.music_source.volume = 0.8f;
        }
        else if(player_credibility.credibility == 4)
        {
            AudioManager.instance.music_source.volume = 0.6f;
        }
        else if(player_credibility.credibility == 3)
        {
            AudioManager.instance.music_source.volume = 0.4f;
        }
        else if(player_credibility.credibility == 2)
        {
            AudioManager.instance.music_source.volume = 0.2f;
        }
        else if(player_credibility.credibility == 1)
        {
            AudioManager.instance.music_source.volume = 0f;
        }
        on_tutorial = false;
        AudioManager.instance.yapping_source.Stop();
    }
}
