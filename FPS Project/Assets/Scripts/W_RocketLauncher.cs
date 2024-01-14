using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_RocketLauncher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform rocketRespawner;
    [SerializeField] GameObject rocketPrefab;
    GameObject rocketInstance;

    [Header("Weapon Variables")]
    [SerializeField] float reloadSpeed;
    [SerializeField] float damage;
    [SerializeField] float shotSpeed;

    private bool isReloaded = true;

    private void Start()
    {
        rocketInstance = Instantiate(rocketPrefab, rocketRespawner);
        rocketInstance.GetComponent<Rocket>().Initialise(damage, shotSpeed);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && isReloaded)
        {
            Fire();
        }
    }

    private void Fire()
    {
        rocketInstance.GetComponent<Rocket>().LaunchRocket();
        isReloaded = false;

        Invoke("Reload", reloadSpeed);
    }

    private void Reload()
    {
        rocketInstance = Instantiate(rocketPrefab, rocketRespawner);
        rocketInstance.GetComponent<Rocket>().Initialise(damage, shotSpeed);
        isReloaded = true;
    }
}
