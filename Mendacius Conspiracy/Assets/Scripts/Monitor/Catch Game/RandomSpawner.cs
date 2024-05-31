using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] catch_prefabs;

    [SerializeField] private float square_width = 2f;
    [SerializeField] private float square_height = 2f;

    public void SpawnRandomObject()
    {
        int random_object = Random.Range(0, catch_prefabs.Length);

        float random_x = Random.Range(-square_width / 2, square_width / 2);
        float random_y = Random.Range(-square_height / 2, square_height / 2);
        Vector3 random_position = new Vector3(random_x, random_y, 0) + transform.position;

        AudioManager.instance.PlaySFX("Throw");
        Instantiate(catch_prefabs[random_object], random_position, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(square_width, square_height, 0));
    }
}
