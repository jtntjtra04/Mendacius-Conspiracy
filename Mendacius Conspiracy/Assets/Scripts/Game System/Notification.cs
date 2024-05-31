using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public static Notification Instance;
    public Text notif_text;

    private GameObject window;
    private Animator notif_anim;

    private Queue<string> notif_queue;
    private Coroutine check_queue;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        window = transform.GetChild(0).gameObject;
        notif_anim = window.GetComponent<Animator>();
        window.SetActive(false);
        notif_queue = new Queue<string>();
    }
    public void AddQueue(string text)
    {
        notif_queue.Enqueue(text);
        if(check_queue == null )
        {
            check_queue = StartCoroutine(CheckQueue());
        }
    }
    private void ShowNotif(string text)
    {
        window.SetActive(true);
        notif_text.text = text;
        notif_anim.Play("Pop_Up");
    }
    private IEnumerator CheckQueue()
    {
        do
        {
            ShowNotif(notif_queue.Dequeue());
            do
            {
                yield return null;
            }
            while (!notif_anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle"));
        }
        while(notif_queue.Count > 0);
        window.SetActive(false);
        check_queue = null;
    }
}
