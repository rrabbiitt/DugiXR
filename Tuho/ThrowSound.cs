using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSound : MonoBehaviour
{
    public AudioClip throwSound1;
    public AudioClip throwSound2;
    public AudioClip throwSound3;
    public AudioClip throwSound4;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // �������� ȿ������ �����Ͽ� ����ϴ� �Լ�
    public void PlayRandomThrowSound()
    {
        AudioClip[] throwSounds = { throwSound1, throwSound2, throwSound3, throwSound4 };

        int randomIndex = Random.Range(0, throwSounds.Length);

        if (throwSounds[randomIndex] != null)
        {
            audioSource.clip = throwSounds[randomIndex];
            audioSource.Play();
        }
    }
}
