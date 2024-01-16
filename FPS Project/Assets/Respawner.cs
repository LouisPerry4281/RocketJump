using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    private void OnEnable()
    {
        CheckPointManager.OnRespawn += RespawnEnemies;
    }

    private void OnDisable()
    {
        CheckPointManager.OnRespawn -= RespawnEnemies;
    }

    void RespawnEnemies()
    {
        transform.position = spawnPoint;
        GetComponentInChildren<MeshRenderer>().enabled = true;
    }
}
