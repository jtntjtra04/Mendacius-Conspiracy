using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArticleData", menuName = "Article/ArticleData")]
public class ArticleData : ScriptableObject
{
    public Sprite image;
    public string author;
    public ArticleType type;
    public AlienWorker alien_worker;
    public DayRange day_range;
}
public enum ArticleType
{
    Fact,
    UniversalHoax,
    HoaxWorker
}
public enum AlienWorker
{
    Anna,
    Haris,
    Lucia,
    Desmond,
    None
}
public enum DayRange
{
    Range1,
    Range2,
    Range3
}
