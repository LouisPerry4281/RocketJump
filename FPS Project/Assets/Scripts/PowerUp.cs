using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] GameObject rocketLauncherReference;
    [SerializeField] AudioManager playerAudioManager;

    private void OnTriggerEnter(Collider other)
    {
        //Check for player layer
        if (other.gameObject.layer == 8)
        {
            EnableRocket();
        }
    }

    private void EnableRocket()
    {
        rocketLauncherReference.SetActive(true);
        playerAudioManager.PlaySound(2);
        Destroy(gameObject);
    }
}
