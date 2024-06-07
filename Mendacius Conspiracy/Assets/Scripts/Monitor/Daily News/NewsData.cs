using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewsData", menuName = "News/NewsData")]
public class NewsData : ScriptableObject
{
    [TextArea] public string day1_title;
    [TextArea] public string day1_content;

/*  [TextArea] public string day1Variation2_title;
    [TextArea] public string day1Variation2_source;
    [TextArea] public string day1Variation2_content;*/

    [TextArea] public string day2_title;
    [TextArea] public string day2_content;

/*  [TextArea] public string day2Variation2_title;
    [TextArea] public string day2Variation2_source;
    [TextArea] public string day2Variation2_content;
*/
    [TextArea] public string day3_title;
    [TextArea] public string day3_content;

/*  [TextArea] public string day3Variation2_title;
    [TextArea] public string day3Variation2_source;
    [TextArea] public string day3Variation2_content;*/

    [TextArea] public string day4_title;
    [TextArea] public string day4_content;

/*  [TextArea] public string day4Variation2_title;
    [TextArea] public string day4Variation2_source;
    [TextArea] public string day4Variation2_content;*/

    [TextArea] public string day5_title;
    [TextArea] public string day5_content;

/*  [TextArea] public string day5Variation2_title;
    [TextArea] public string day5Variation2_source;
    [TextArea] public string day5Variation2_content;*/

    [TextArea] public string day6_title;
    [TextArea] public string day6_content;

/*  [TextArea] public string day6Variation2_title;
    [TextArea] public string day6Variation2_source;
    [TextArea] public string day6Variation2_content;*/

    [TextArea] public string day7_title;
    [TextArea] public string day7_content;

/*  [TextArea] public string day7Variation2_title;
    [TextArea] public string day7Variation2_source;
    [TextArea] public string day7Variation2_content;*/

    public (string title, string content) GetDailyNews(int day)
    {
        switch(day)
        {
            case 1: return (day1_title, day1_content);
            case 2: return (day2_title, day2_content);
            case 3: return (day3_title, day3_content);
            case 4: return (day4_title, day4_content);
            case 5: return (day5_title, day5_content);
            case 6: return (day6_title, day6_content);
            case 7: return (day7_title, day7_content);
            default: return ("","");
        }
    }
}
