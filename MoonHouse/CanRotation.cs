using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanRotation : MonoBehaviour
{
    public float rotationSpeed = 0f; // 회전 속도
    public float rotationSpeedChangeSpeed = 5f;
    public Transform centerObject;    // 중심점을 나타내는 하위 오브젝트의 Transform

    public ParticleSystem explosionPrefab;
    public AudioClip explosionSound;

    public GameObject Fire01Prefab;
    public GameObject Fire02Prefab;
    public GameObject Fire03Prefab;

    public int speedLevel = 0;

    void Update()
    {
        SpeedCalculator speedCalculator = FindObjectOfType<SpeedCalculator>();

        if (speedCalculator != null)
        {
            // SpeedCalculator에서 구한 speedLevel 값 가져오기
            speedLevel = speedCalculator.GetSpeedLevel();

            // SpeedCalculator에서 구한 speedLevel 값 가져오기
            int newSpeedLevel = speedCalculator.GetSpeedLevel();

            // speedLevel에 따라 rotationSpeed 설정 (서서히 변경)
            float targetSpeed = GetSpeedForLevel(newSpeedLevel);
            rotationSpeed = Mathf.Lerp(rotationSpeed, targetSpeed, rotationSpeedChangeSpeed * Time.deltaTime);

            UpdateFirePrefabs(newSpeedLevel);

            //Debug.Log("rotationSpeed: " + rotationSpeed);

            if (newSpeedLevel == 4)
            {
                // 폭발 이펙트 재생
                PlayExplosionEffect();
                // 1초 후에 파괴 및 재생성
                Invoke("DestroyAndRespawn", 1f);
            }
        }

        // 중심으로 사용할 위치 계산
        Vector3 pivot = centerObject.position;

        // z 축을 중심으로 360도 회전
        transform.RotateAround(pivot, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    public int GetSpeedLevel()
    {
        return speedLevel;
    }

    void UpdateFirePrefabs(int level)
    {
        if (level >= 0 && level <= 1)
        {
            Fire01Prefab.SetActive(false);
            Fire02Prefab.SetActive(false);
            Fire03Prefab.SetActive(false);
        }
        else
        {
            Fire01Prefab.SetActive(level == 1 || level == 0);
            Fire02Prefab.SetActive(level == 2);
            Fire03Prefab.SetActive(level == 3);
        }

        // rotationspeed가 0이면 Fire01Prefab 비활성화
        if (Mathf.Approximately(rotationSpeed, 0f))
        {
            Fire01Prefab.SetActive(false);
        }
    }

    float GetSpeedForLevel(int level)
    {
        switch (level)
        {
            case 0:
                return 0f;
            case 1:
                return 1000f;
            case 2:
                return 2000f;
            case 3:
                return 3000f;
            case 4:
                return 0f; // 폭발 이펙트 후 초기화
            default:
                return 0f;
        }
    }

    void PlayExplosionEffect()
    {
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        // 폭발 이펙트 재생
        ParticleSystem explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 1초 뒤에 폭발 이펙트 파괴
        Destroy(explosionEffect.gameObject, 1f);
    }

    void DestroyAndRespawn()
    {
        // CansSpawner 스크립트를 가진 오브젝트를 찾아서 SpawnCan 메서드 호출
        CanSpawner canSpawner = FindObjectOfType<CanSpawner>();
        if (canSpawner != null)
        {
            canSpawner.SpawnCan();
        }

        // 현재 게임 오브젝트 파괴
        Destroy(gameObject);
    }

    public void GameOverDestroy()
    {
        Destroy(gameObject);
    }
}
