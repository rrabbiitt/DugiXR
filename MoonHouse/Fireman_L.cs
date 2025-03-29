using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireman_L : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도를 조절할 변수를 추가합니다.
    public GameObject fireEffectPrefab; // 불 이펙트 프리팹을 연결해야 합니다.

    private void Update()
    {
        // Fireman을 z+ 방향으로 이동합니다.
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // 만약 Fireman의 y 좌표가 0 이하로 내려가면 풍덩 소리를 재생하고 파괴합니다.
        if (transform.position.y <= 22)
        {
            //PlaySplashSound();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 만약 Fireman이 Log 콜라이더와 충돌하면 불 이펙트를 생성하고 파괴합니다.
        if (other.CompareTag("Log"))
        {
            SpawnFireEffect();

            Timer timerScript = FindObjectOfType<Timer>();
            if (timerScript != null)
            {
                timerScript.UpdateScore();
            }

            Destroy(gameObject);
        }
    }

    private void SpawnFireEffect()
    {
        // 불 이펙트를 생성합니다.
        if (fireEffectPrefab != null)
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}
