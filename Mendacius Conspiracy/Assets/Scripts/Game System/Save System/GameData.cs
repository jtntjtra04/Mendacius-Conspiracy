using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int troll;
    public float credibility;
    public float action_point;
    public int day;
    public int time;
    public bool today_news_accessed;
    public int curr_fact;
    public int curr_catch;
    public bool new_game;
    public GameData()
    {
        this.day = 1;
        this.time = 16;
        this.credibility = 5f;
        this.action_point = 8f;
        this.today_news_accessed = false;
        this.curr_fact = 0;
        this.curr_catch = 0;
        this.new_game = true;
    }
    public void Initialize()
    {
        this.troll = Random.Range(0, 3);
    }
}
