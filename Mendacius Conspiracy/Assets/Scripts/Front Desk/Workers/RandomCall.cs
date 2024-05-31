using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCall : MonoBehaviour
{
    public GameObject[] workers;
    public int troll_index;
    public int call_index;
    [SerializeField] private Transform call_point;
    public GameObject worker_option;
    public bool on_interrogation = false;

    private void Start()
    {
        troll_index = Random.Range(0, workers.Length); // Randomly add 1 troll from the workers
        worker_option.SetActive(false);
        Debug.Log(troll_index);
    }
    public void OpenWorkerOption()
    {
        AudioManager.instance.PlaySFX("Click");
        worker_option.SetActive(true);
    }
    public void CloseWorkerOption()
    {
        AudioManager.instance.PlaySFX("Click");
        worker_option.SetActive(false);
    }
    public void CallWorker1()
    {
        if (!on_interrogation)
        {
            call_index = 0;
            StartCoroutine(RingTheBell(5f));
        }
        else
        {
            Notification.Instance.AddQueue("Currently call worker");
        }
    }
    public void CallWorker2()
    {
        if (!on_interrogation)
        {
            call_index = 1;
            StartCoroutine(RingTheBell(5f));
        }
        else
        {
            Notification.Instance.AddQueue("Currently call worker");
        }
    }
    public void CallWorker3()
    {
        if (!on_interrogation)
        {
            call_index = 2;
            StartCoroutine(RingTheBell(5f));
        }
        else
        {
            Notification.Instance.AddQueue("Currently call worker");
        }
    }
    public void CallWorker4()
    {
        if (!on_interrogation)
        {
            call_index = 3;
            StartCoroutine(RingTheBell(5f));
        }
        else
        {
            Notification.Instance.AddQueue("Currently call worker");
        }
    }
    public void CallRandomWorker()
    {
        if (!on_interrogation)
        {
            call_index = Random.Range(0, workers.Length);
            Instantiate(workers[call_index], call_point);
            on_interrogation = true;
        }
    }
    private IEnumerator RingTheBell(float ringtone_duration)
    {
        on_interrogation = true;
        worker_option.SetActive(false);
        AudioManager.instance.hybrid_source.loop = true;
        AudioManager.instance.PlayHybrid("CallWorker");
        yield return new WaitForSeconds(ringtone_duration);
        AudioManager.instance.hybrid_source.Stop();
        AudioManager.instance.hybrid_source.loop = false;
        Instantiate(workers[call_index], call_point);
    }
    public int GetCurrentWorkerIndex()
    {
        return call_index;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            worker_option.SetActive(false);
        }
    }
}
