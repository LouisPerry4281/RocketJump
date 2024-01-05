using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float aggroRange;
    public float reloadTime;

    bool isReloaded = true;

    Transform playerTrans;

    private void Awake()
    {
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (InRange() && isReloaded)
            StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        isReloaded = false;

        //Shoot Logic
        print("Shoot");

        yield return new WaitForSeconds(reloadTime);

        isReloaded = true;

        yield return null;
    }

    private bool InRange()
    {
        if (Vector3.Distance(playerTrans.position, transform.position) < aggroRange)
        {
            return true;
        }

        else return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, aggroRange);

    }
}
