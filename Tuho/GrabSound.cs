using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSound : MonoBehaviour
{
    public AudioClip grabSound; // Inspector���� AudioClip�� �Ҵ��� �� �ֵ��� public ������ ����
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
