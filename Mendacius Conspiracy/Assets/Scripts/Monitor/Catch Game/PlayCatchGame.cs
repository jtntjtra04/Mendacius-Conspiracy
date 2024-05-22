using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class PlayCatchGame : MonoBehaviour
{
    public GameObject start_panel;
    public RandomSpawner random_spawn;
    public PlayerMovement player_movement;
    public GameObject end_panel;
    [SerializeField] private float game_duration = 60f;

    // UI Status
    public Text timer_text;

    // Score References
    private ScoreManager score_manager;
    public float target_score;
    public Text end_score;
    public Text end_description;
    public Text penalty;

    private void Start()
    {
        score_manager = GetComponent<ScoreManager>();
    }
    public void StartGame()
    {
        start_panel.SetActive(false);
        player_movement.player_active = true;
        timer_text.text = game_duration.ToString();
        StartCoroutine(GameReady(3));
        StartCoroutine(GameStart());
    }
    private IEnumerator GameReady(int ready_time)
    {
        yield return new WaitForSeconds(ready_time);
    }
    private IEnumerator GameStart()
    {
        float timer = game_duration;

        while (timer > 0)
        {
            if(timer <= 20)
            {
                random_spawn.SpawnRandomObject();
                yield return new WaitForSeconds(0.251f);
            }
            else if(timer <= 40)
            {
                random_spawn.SpawnRandomObject();
                yield return new WaitForSeconds(0.5f);
            }
            else if(timer <= 60)
            {
                random_spawn.SpawnRandomObject();
                yield return new WaitForSeconds(1f);
            }
            timer -= 1f;
            UpdateTime(timer);
        }
        EndGame();

    }
    public void EndGame()
    {
        end_panel.SetActive(true);
        player_movement.player_active = false;
        end_score.text = "Score : " + score_manager.score.ToString();
        Judgement();
    }
    private void UpdateTime(float time)
    {
        timer_text.text = time.ToString();
    }
    private void Judgement()
    {
        if(score_manager.score >= target_score)
        {
            end_description.text = "You have reached your target score";
        }
        else
        {
            end_description.text = "You didn't reach your target score : ";
            penalty.text = "Penalty : -1 Credibility";
        }
        // Exit Game logic
    }
}
