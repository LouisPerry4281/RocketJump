using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float damage;

    private void Start()
    {
        Invoke(nameof(DestroyThis), 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        //If this collides with the player
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(damage);
            DestroyThis();
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
