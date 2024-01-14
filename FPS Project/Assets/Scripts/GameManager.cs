using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                KillEnemy();
                break;

            case 7: //Player Layer
                KillPlayer();
                break;

            case 8: //Random Object Layer
                KillObject();
                break;
        }
    }
    
    private void KillObject()
    {
        //throw new NotImplementedException();
    }

    private void KillPlayer()
    {
        checkPointManager.RespawnPlayer();
    }

    private void KillEnemy()
    {
        //throw new NotImplementedException();
    }
}
