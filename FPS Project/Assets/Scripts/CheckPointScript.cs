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
            SetCheckpoint();
        }
    }

    public void SetCheckpoint()
    {
        gameManager.GetComponent<CheckPointManager>().ChangeSpawnLocation(transform.position);
        gameManager.GetComponent<UIManager>().CheckpointReached();
        gameObject.SetActive(false);
    }
}
