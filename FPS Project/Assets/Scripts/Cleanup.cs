using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour
{
    [SerializeField] float cleanupTime;

    private void Start()
    {
        Invoke(nameof(CleanUp), cleanupTime);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }
}
