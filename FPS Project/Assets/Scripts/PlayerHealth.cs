using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    GameManager gameManager;
    AudioManager audioManager;

    [SerializeField] float maxHealth;
    [SerializeField] float healingRate;
    [SerializeField] float recoveryDelay;

    float currentHealth;
    bool fullHealth = true;

    float outOfDangerTimer;
    bool outOfDanger = true;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GetComponent<AudioManager>();

        currentHealth = maxHealth;
    }

    private void Update()
    {
        OutOfDangerCheck();

        if (outOfDanger && !fullHealth)
        {
            HealPlayer();
            if (currentHealth >= maxHealth)
            {
                fullHealth = true;
                currentHealth = maxHealth;
            }
        }
    }

    private void OutOfDangerCheck()
    {
        outOfDangerTimer -= Time.deltaTime;

        if (outOfDangerTimer <= 0)
        {
            outOfDanger = true;
        }

        else
            outOfDanger = false;
    }

    private void HealPlayer()
    {
        currentHealth += healingRate;
    }

    public void TakeDamage(float damageToTake)
    {
        print("Ouchies: " + damageToTake + " damage!");
        currentHealth -= damageToTake;
        outOfDangerTimer = recoveryDelay;

        audioManager.PlaySound(1);

        CheckHealth();
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            gameManager.ScanKillTarget(gameObject);
            currentHealth = maxHealth;
        }

        if (currentHealth < maxHealth)
        {
            fullHealth = false;
        }
    }
}
