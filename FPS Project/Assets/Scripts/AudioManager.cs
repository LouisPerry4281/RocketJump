using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] audioClips;

    public void PlaySound(int soundIndex)
    {
        audioClips[soundIndex].Play();
    }
}
