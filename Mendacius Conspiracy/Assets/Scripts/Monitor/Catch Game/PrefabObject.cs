using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabObject : MonoBehaviour
{
    private float lifetime_duration = 7f;

    private void Start()
    {
        Destroy(gameObject, lifetime_duration);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
