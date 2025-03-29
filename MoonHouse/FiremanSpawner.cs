using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiremanSpawner : MonoBehaviour
{
    public GameObject firemanLPrefab; // FIREMAN_L 프리팹을 연결해야 합니다.
    public GameObject firemanRPrefab; // FIREMAN_R 프리팹을 연결해야 합니다.
    public Vector3 spawnPosition1; // 첫 번째 소환 위치
    public Vector3 spawnPosition2; // 두 번째 소환 위치

    private float spawnTimer = 0f; // 소환 타이머
    private bool spawnAtPosition1 = true; // 첫 번째 위치에 소환할지 두 번째 위치에 소환할지를 결정하는 플래그

    private void Update()
    {
        // 매 프레임마다 소환 타이머를 증가시킵니다.
        spawnTimer += Time.deltaTime;

        // 소환 타이머가 2초 이상일 때마다 Fireman을 소환하고 타이머를 재설정합니다.
        if (spawnTimer >= 2f)
        {
            SpawnFireman();
            spawnTimer = 0f; // 타이머 초기화
        }
    }

    private void SpawnFireman()
    {
        if (spawnAtPosition1)
        {
            if (firemanLPrefab != null)
            {
                Instantiate(firemanLPrefab, spawnPosition1, Quaternion.identity);
            }
        }
        else
        {
            if (firemanRPrefab != null)
            {
                // Fireman_R 프리팹을 소환할 때 180도 회전하여 소환합니다.
                GameObject fireman = Instantiate(firemanRPrefab, spawnPosition2, Quaternion.Euler(0f, 180f, 0f));
            }
        }

        spawnAtPosition1 = !spawnAtPosition1; // 플래그를 토글하여 다음 소환 위치를 결정합니다.
    }
}
