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
            // 목표 오브젝트와 충돌 시 성공 효과음 재생
            AudioSource.PlayClipAtPoint(successSound, transform.position);

            // 폭발 이펙트 재생
            ParticleSystem explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // 1초 뒤에 폭발 이펙트 파괴
            Destroy(explosionEffect.gameObject, 1f);

            Destroy(gameObject);
        }

        if (other.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
