using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCan : MonoBehaviour
{
    public ParticleSystem explosionPrefab;
    public AudioClip successSound;
    public AudioClip flyingSound;
    private AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cliff"))
        {
            // ��ǥ ������Ʈ�� �浹 �� ���� ȿ���� ���
            AudioSource.PlayClipAtPoint(successSound, transform.position);

            // ���� ����Ʈ ���
            ParticleSystem explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // 1�� �ڿ� ���� ����Ʈ �ı�
            Destroy(explosionEffect.gameObject, 1f);

            Destroy(gameObject);
        }

        if (other.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
