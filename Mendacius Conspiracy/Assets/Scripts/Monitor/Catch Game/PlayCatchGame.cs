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
    private float spawn_interval = 0f;

    // UI Status
    public Text timer_text;

    // Score References
    private ScoreManager score_manager;
    public float target_score;
    public Text end_score;
    public Text end_description;
    public Text penalty;
    public int score_today;

    // Condition
    public bool catchgame_active = false;

    // Status References
    public ActionPoint player_AP;
    public Credibility player_credibility;
    public TimeSystem time_system;

    private void Start()
    {
        score_manager = GetComponent<ScoreManager>();
    }
    public void StartGame()
    {
        if(player_AP.has_AP)
        {
            start_panel.SetActive(false);
            player_movement.player_active = true;
            timer_text.text = game_duration.ToString();
            catchgame_active = true;
            player_AP.UseActionPoint();
            StartCoroutine(GameReady(3));
            StartCoroutine(GameStart());
        }
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
                spawn_interval = 0.1f;
                while(spawn_interval < 1f)
                {
                    random_spawn.SpawnRandomObject();
                    yield return new WaitForSeconds(spawn_interval);
                    spawn_interval = spawn_interval + spawn_interval;
                }
            }
            else if(timer <= 40)
            {
                spawn_interval = 0.3f;
                while (spawn_interval < 1f)
                {
                    random_spawn.SpawnRandomObject();
                    yield return new WaitForSeconds(spawn_interval);
                    spawn_interval = spawn_interval + spawn_interval;
                }
            }
            else if(timer <= 60)
            {
                spawn_interval = 1f;
                random_spawn.SpawnRandomObject();
                yield return new WaitForSeconds(spawn_interval);
            }
            timer -= Time.timeScale;
            UpdateTimer(timer);
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
    private void UpdateTimer(float time)
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
            player_credibility.MinusCredibility(1);
        }
        time_system.UpdateTime();
        SaveScore();
    }
    private void SaveScore()
    {
        score_today = score_manager.score;
    }
}
