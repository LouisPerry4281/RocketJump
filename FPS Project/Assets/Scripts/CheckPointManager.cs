using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public delegate void RespawnAction();
    public static event RespawnAction OnRespawn;

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
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (OnRespawn != null)
            OnRespawn();
    }

}
