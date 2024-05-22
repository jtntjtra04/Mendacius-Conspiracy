using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Worker : MonoBehaviour
{
    // To get identity
    RandomCall identity;
    private bool is_troll = false;
    private int current_worker;

    // Worker movement
    private float speed = 8f;
    Transform stop_point;

    private void Start()
    {
        WorkerReferences worker_references = FindObjectOfType<WorkerReferences>();
        identity  = worker_references.identity;
        stop_point = worker_references.stop_point;

        current_worker = identity.GetCurrentWorkerIndex();

        if(current_worker == identity.troll_index)
        {
            is_troll = true;
            Debug.Log("This Worker is a Troll");
        }
        else
        {
            is_troll = false;
            Debug.Log("This Worker is Normal");
        }
    }

    private void Update()
    {
        float move_step = speed * Time.deltaTime;

        if(transform.position.x > stop_point.position.x)
        {
            transform.Translate(move_step * -1f, 0, 0);
        }
        else if(transform.position.x == stop_point.position.x)
        {

        }
    }
}
