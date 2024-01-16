using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    CheckPointManager checkPointManager;

    private void Start()
    {
        checkPointManager = GetComponent<CheckPointManager>();
    }

    public void ScanKillTarget(GameObject objToKill)
    {
        switch(objToKill.layer)
        {
            case 6: //Enemy Layer
                KillEnemy(objToKill);
                break;

            case 8: //Player Layer
                KillPlayer();
                break;

            case 7: //Random Object Layer
                KillObject(objToKill);
                break;
        }
    }
    
    private void KillObject(GameObject debris)
    {
        debris.transform.position = new Vector3(1000, 1000, 1000);
        debris.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    private void KillPlayer()
    {
        checkPointManager.RespawnPlayer();
    }

    private void KillEnemy(GameObject enemy)
    {
        enemy.transform.position = new Vector3(1000, 1000, 1000);
        enemy.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    public void WinSequence()
    {
        GetComponent<UIManager>().WinText();
        Invoke(nameof(MainMenu), 3);
    }

    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
