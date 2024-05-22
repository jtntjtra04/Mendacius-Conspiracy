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

    private void Start()
    {
        troll_index = Random.Range(0, workers.Length); // Randomly add 1 troll from the workers
        worker_option.SetActive(false);
    }
    public void OpenWorkerOption()
    {
        worker_option.SetActive(true);
    }
    public void CloseWorkerOption()
    {
        worker_option.SetActive(false);
    }
    public void CallWorker1()
    {
        call_index = 0;
        StartCoroutine(RingTheBell(5f));
    }
    public void CallWorker2()
    {
        call_index = 1;
        StartCoroutine(RingTheBell(5f));
    }
    public void CallWorker3()
    {
        call_index = 2;
        StartCoroutine(RingTheBell(5f));
    }
    public void CallWorker4()
    {
        call_index = 3;
        StartCoroutine(RingTheBell(5f));
    }
    private IEnumerator RingTheBell(float ringtone_duration)
    {
        worker_option.SetActive(false);
        yield return new WaitForSeconds(ringtone_duration);
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
