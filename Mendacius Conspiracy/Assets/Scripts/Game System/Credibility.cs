using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class Credibility : MonoBehaviour
{
    public float credibility;

    // References 
    private ActionPoint action_point;
    public PostProcessing post_processing;
    public Animator fade_transition;
    public Animator ending_scene;
    private void Awake()
    {
        action_point = GetComponent<ActionPoint>();
    }
    private void Start()
    {
        credibility = 5;
    }
    public void MinusCredibility(float penalty)
    {
        credibility -= penalty;
        post_processing.UpdateSaturation();
        AudioManager.instance.DecreaseMusicVolume();
        AudioManager.instance.IncreaseHorrorMusicVolume();
        
        if (credibility <= 0)
        {
            float chance_jumpscare = Random.value;
            if(chance_jumpscare <= 0.4 && !action_point.hardjumpscare_on)
            {
                action_point.HardJumpscareTrigger();
            }
            GameOver();
        }
    }
    private void GameOver()
    {
        StartCoroutine(BadEnding());
    }
    private IEnumerator BadEnding()
    {
        AudioManager.instance.music_source.Stop();
        yield return new WaitForSeconds(3f);
        post_processing.ChangeRedScene();
        yield return new WaitForSeconds(3f);
        fade_transition.Play("Start_Fade");
        post_processing.ResetColorScene();
        yield return new WaitForSeconds(4f);
        ending_scene.Play("BadEnding_Start");
        AudioManager.instance.background_source.Stop();
        AudioManager.instance.hybrid_source.Stop();

        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        ending_scene.Play("BadEnding_End");
        SceneManager.LoadScene("MainMenu");
    }
}
