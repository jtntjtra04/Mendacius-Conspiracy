using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Credibility : MonoBehaviour
{
    public float credibility;
    private void Start()
    {
        credibility = 5;
    }
    public void MinusCredibility(float penalty)
    {
        credibility -= penalty;
        
        if (credibility <= 0 )
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        // GameOver Logic
    }
    void UpdateSceneColor()
    {
        float saturation = 1.0f - (credibility * 0.2f);
        
    }
}
