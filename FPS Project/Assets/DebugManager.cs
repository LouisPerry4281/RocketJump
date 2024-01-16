using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField] GameObject[] checkPoints;
    [SerializeField] GameObject playerRocketLauncher;

    public void Update()
    {
        TakeInput();
    }

    void DebugRespawn(int index)
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].SetActive(true);

            if (i == index)
            {
                checkPoints[i].GetComponent<CheckPointScript>().SetCheckpoint();

                if (playerRocketLauncher.activeInHierarchy == false && i >= 4)
                    playerRocketLauncher.SetActive(true);
                else if (playerRocketLauncher.activeInHierarchy == true && i < 4)
                    playerRocketLauncher.SetActive(false);
            }
        }

        GetComponent<CheckPointManager>().RespawnPlayer();
    }

    private void TakeInput()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            DebugRespawn(0);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            DebugRespawn(1);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            DebugRespawn(2);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            DebugRespawn(3);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            DebugRespawn(4);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha6))
        {
            DebugRespawn(5);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha7))
        {
            DebugRespawn(6);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha8))
        {
            DebugRespawn(7);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha9))
        {
            DebugRespawn(8);
        }
    }
}
