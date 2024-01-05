using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] LayerMask screenLayer;

    private float damageNum;
    private float shotSpeedNum;

    [Header("Explosion Variables")]
    [SerializeField] float explosionForce;
    [SerializeField] float explosionRadius;
    [SerializeField] float verticalForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        //Check for ground or enemy hit
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            Explode();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //Check for ground or enemy hit
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            Explode();
        }
    }

    private void Explode()
    {
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

            EnemyHealth enemyHealth = inRange.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageNum);
            }

            if (targetRB != null)
            {
                targetRB.AddExplosionForce(explosionForce, transform.position, explosionRadius, verticalForce, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
