using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFloor : MonoBehaviour
{
    //This will be a 1-hit ko for now, will leave a variable just in case
    [SerializeField] float lavaDamage;

    private void OnCollisionEnter(Collision collision)
    {
        //Player Layer
        if (collision.collider.gameObject.layer == 8)
        {
            collision.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(lavaDamage);
        }
    }
}
