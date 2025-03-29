using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSound : MonoBehaviour
{
    public AudioClip grabSound; // Inspector에서 AudioClip을 할당할 수 있도록 public 변수로 선언
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlayGrabSound()
    {
        if (grabSound != null)
        {
            audioSource.clip = grabSound;
            audioSource.Play();
        }
    }
}
