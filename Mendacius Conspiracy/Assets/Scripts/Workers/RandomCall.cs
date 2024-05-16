using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCall : MonoBehaviour
{
    public GameObject[] workers;
    public int troll_index;
    public int random_call;
    [SerializeField] private Transform call_point;

    private void Start()
    {
        troll_index = Random.Range(0, workers.Length); // Randomly add 1 troll from the workers
        //workers[troll_index].
    }
    public void CallWorkers()
    {
        random_call = Random.Range(0, workers.Length);
        Instantiate(workers[random_call], call_point);
    }
}
