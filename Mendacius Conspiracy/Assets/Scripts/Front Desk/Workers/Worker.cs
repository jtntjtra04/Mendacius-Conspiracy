using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Worker : MonoBehaviour
{
    // To get identity
    RandomCall identity;
    public bool is_troll = false;
    public int current_worker;

    // Worker movement
    private float speed = 8f;
    Transform stop_point;
    Vector3 call_point;
    private bool can_move;
    private bool moving_in;

    // Worker State
    public Sprite tired_state;
    public Sprite crazed_state;
    private SpriteRenderer worker_sprite;

    // Dialogue References
    private DialogueManager dialogue_manager;

    // Trigger Command
    private bool command_triggered;

    // Other References
    private Credibility cred;

    private void Start()
    {
        worker_sprite = GetComponent<SpriteRenderer>();
        cred = FindAnyObjectByType<Credibility>();

        command_triggered = false;
        can_move = true;
        moving_in = true;
        WorkerReferences worker_references = FindAnyObjectByType<WorkerReferences>();
        identity  = worker_references.identity;
        stop_point = worker_references.stop_point;
        call_point = transform.position; // Store initial position

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
        // Dialogue System
        dialogue_manager = GetComponent<DialogueManager>();

        if(dialogue_manager != null)
        {
            float random_value = Random.value;

            if(is_troll && random_value <= 0.35f)
            {
                dialogue_manager.SetWorkerState("troll");
                Debug.Log("Troll Dialogue");
            }
            else
            {
                if(random_value <= 0.2f)
                {
                    dialogue_manager.SetWorkerState("tired");
                    Debug.Log("Tired Dialogue");
                }
                else
                {
                    dialogue_manager.SetWorkerState("normal");
                    Debug.Log("Normal Dialogue");
                }
            }
        }
    }
    private void TriggerCommand()
    {
        if (!command_triggered)
        {
            CommandManager command_manager = FindAnyObjectByType<CommandManager>();

            if (!command_manager.command_active)
            {
                command_manager.OpenCommand();
            }
            command_triggered = true;
            can_move = false; // stop movement
        }
    }

    private void Update()
    {
        if(can_move)
        {
            float move_step = speed * Time.deltaTime;

            if (moving_in)
            {
                if (transform.position.x < stop_point.position.x) //move toward stop point
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    transform.Translate(move_step, 0, 0);
                }
                else if (transform.position.x >= stop_point.position.x)
                {
                    if (current_worker == 0)
                    {
                        AudioManager.instance.PlaySFX("AnnaHello");
                    }
                    else if (current_worker == 1)
                    {
                        AudioManager.instance.PlaySFX("HarisHello");
                    }
                    else if (current_worker == 2)
                    {
                        AudioManager.instance.PlaySFX("LuciaHello");
                    }
                    else if (current_worker == 3)
                    {
                        AudioManager.instance.PlaySFX("DesmondHello");
                    }
                    TriggerCommand();
                }
            }
            else
            {
                if(transform.position.x > call_point.x - 3f)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    transform.Translate(-move_step, 0, 0);
                }
                else
                {
                    can_move = false; // stop movement
                    Destroy(gameObject, 1f);
                }
            }
        }
        if(cred.credibility < 5)
        {
            UpdateWorkerState();
        }
    }
    private void UpdateWorkerState()
    {
        if(cred.credibility <= 1)
        {
            worker_sprite.sprite = crazed_state;
        }
        else if(cred.credibility <= 4 && cred.credibility >= 2)
        {
            worker_sprite.sprite = tired_state;
        }
    }
    public void StartMovingBack() // Reset movement
    {
        command_triggered = false;
        can_move = true;
        moving_in = false; // Change direction to call point
    }
}
