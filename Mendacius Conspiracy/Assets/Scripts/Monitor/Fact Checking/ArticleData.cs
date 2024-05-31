using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArticleData", menuName = "Article/ArticleData")]
public class ArticleData : ScriptableObject
{
    public string title;
    public string author;
    [TextArea(10, 10)]
    public string content1;
    [TextArea(10, 10)]
    public string content2;
    public Sprite image;
    public ArticleType type;
    public AlienWorker alien_worker;
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
