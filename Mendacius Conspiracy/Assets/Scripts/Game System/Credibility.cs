using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credibility : MonoBehaviour
{
    public int credibility;
    private void Start()
    {
        credibility = 5;
    }
    public void MinusCredibility(int penalty)
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
}
