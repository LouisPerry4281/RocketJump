using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    Rigidbody rb;
    GameManager gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /* //Design Change, this may not be useful//
    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0)
        {
            DeathState();
        }
    }

    private void DeathState()
    {
        rb.isKinematic = false;

        gameManager.ScanKillTarget(gameObject);
    }
    */
}
