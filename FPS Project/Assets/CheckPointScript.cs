using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check for player later
        if (other.gameObject.layer == 8)
        {
            gameManager.GetComponent<CheckPointManager>().ChangeSpawnLocation(transform.position);
            Destroy(gameObject);
        }
    }
}
