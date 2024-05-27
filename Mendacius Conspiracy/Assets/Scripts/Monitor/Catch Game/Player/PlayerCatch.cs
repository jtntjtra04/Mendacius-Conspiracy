using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatch : MonoBehaviour
{
    private PlayerMovement player_movement;
    private void Awake()
    {
        player_movement = GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player_movement.player_active)
        {
            if (collision.CompareTag("Fact"))
            {
                ScoreManager.instance.AddScore(10);
                ScoreManager.instance.AddFactCount(1);
            }
            if (collision.CompareTag("Hoax"))
            {
                ScoreManager.instance.SubstractScore(5);
                ScoreManager.instance.AddHoaxCount(1);
            }
            if (collision.CompareTag("SuperFact"))
            {
                ScoreManager.instance.AddScore(15);
                ScoreManager.instance.AddSuperFactCount(1);
            }
            if (collision.CompareTag("FatalHoax"))
            {
                ScoreManager.instance.SubstractScore(10);
                ScoreManager.instance.AddFatalHoaxCount(1);
            }
        }
    }
}
