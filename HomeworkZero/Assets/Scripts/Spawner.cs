using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // press Space to spawn
        {
            Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }
}
