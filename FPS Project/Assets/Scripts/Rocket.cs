using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameObject explosionObject;
    AudioManager rocketAudioManager;
    GameObject gameManager;

    private float damageNum;
    private float shotSpeedNum;
    private bool hasExploded = false;

    [Header("Explosion Variables")]
    [SerializeField] float explosionForce;
    [SerializeField] float explosionRadius;
    [SerializeField] float verticalForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketAudioManager = GameObject.Find("RocketLauncher").GetComponent<AudioManager>();

        gameManager = GameObject.Find("GameManager");
    }

    public void Initialise(float damage, float shotSpeed)
    {
        damageNum = damage;
        shotSpeedNum = shotSpeed;
    }

    public void LaunchRocket()
    {
        rb.AddForce(transform.right * shotSpeedNum, ForceMode.Impulse);
        transform.parent = null;
        GetComponent<BoxCollider>().enabled = true;

        Invoke("Explode", 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasExploded)
            return;

        //Check for ground, enemy, object or wall is hit
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6 || collision.gameObject.layer == 7 || collision.gameObject.layer == 9)
        {
            Explode();
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        hasExploded = true;

        Instantiate(explosionObject, transform.position, Quaternion.identity);

        gameManager.GetComponent<CameraShake>().shakeDuration = 0.4f;
        gameManager.GetComponent<CameraShake>().shakeAmount = 0.1f;

        rocketAudioManager.PlaySound(1);

        GetComponent<MeshRenderer>().enabled = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider inRange in colliders)
        {
            Rigidbody targetRB = inRange.GetComponent<Rigidbody>();
            if (inRange.gameObject.name == "PlayerCollider")
            {
                inRange.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, verticalForce, ForceMode.Impulse);
                return;
            }

            else if (targetRB != null)
            {
                targetRB.AddExplosionForce(explosionForce, transform.position, explosionRadius, verticalForce, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
