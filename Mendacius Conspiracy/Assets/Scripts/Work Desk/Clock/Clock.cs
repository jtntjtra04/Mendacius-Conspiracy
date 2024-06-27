using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private TimeSystem time_system;

    private SpriteRenderer curr_sprite;
    [SerializeField] private Sprite four_pm;
    [SerializeField] private Sprite five_pm;
    [SerializeField] private Sprite six_pm;
    [SerializeField] private Sprite seven_pm;
    [SerializeField] private Sprite eight_pm;
    [SerializeField] private Sprite nine_pm;
    [SerializeField] private Sprite ten_pm;
    [SerializeField] private Sprite eleven_pm;
    [SerializeField] private Sprite twelve_pm;

    private void Awake()
    {
        curr_sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(time_system.time == 16)
        {
            curr_sprite.sprite = four_pm;
        }
        else if(time_system.time == 17)
        {
            curr_sprite.sprite = five_pm;
        }
        else if (time_system.time == 18)
        {
            curr_sprite.sprite = six_pm;
        }
        else if (time_system.time == 19)
        {
            curr_sprite.sprite = seven_pm;
        }
        else if (time_system.time == 20)
        {
            curr_sprite.sprite = eight_pm;
        }
        else if (time_system.time == 21)
        {
            curr_sprite.sprite = nine_pm;
        }
        else if (time_system.time == 22)
        {
            curr_sprite.sprite = ten_pm;
        }
        else if (time_system.time == 23)
        {
            curr_sprite.sprite = eleven_pm;
        }
        else
        {
            curr_sprite.sprite = twelve_pm;
        }
    }
}
