using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    Vector3 respawnPos;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        respawnPos = player.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RespawnPlayer();
    }

    public void ChangeSpawnLocation(Vector3 newPos)
    {
        respawnPos = newPos;
    }

    public void RespawnPlayer()
    {
        player.transform.position = respawnPos;
        //Add Enemy Spawn Later
    }

}
