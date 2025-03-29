using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public GameObject monster01Prefab; // 몬스터 프리팹
    public GameObject monster02Prefab;
    public AudioClip spawnSound;
    private Transform player;
    private Transform face;
    private bool isSpawning = true; // 몬스터 생성 여부를 제어하는 부울 변수

    void Start()
    {
        Invoke("SpawnOnce", 3f);
        InvokeRepeating("Spawn", 8f, 5f);
        player = Camera.main.transform;
    }

    void SpawnOnce()
    {
        Spawn();
    }

    void Spawn()
    {
        if (!isSpawning) return; // 몬스터 생성 여부 확인

        GameObject selectedMonsterPrefab = Random.Range(0, 2) == 0 ? monster01Prefab : monster02Prefab;

        float randomX = Random.Range(-2f, 2f);
        float randomY = Random.Range(-0.5f, 0.5f);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 3f);

        GameObject monster = Instantiate(selectedMonsterPrefab, spawnPosition, Quaternion.identity);
        AudioSource.PlayClipAtPoint(spawnSound, spawnPosition);

        LookAtCamera(monster);
    }

    void LookAtCamera(GameObject monster)
    {
        Transform monsterFace = monster.transform.Find("MonsterFace");
        if (monsterFace != null)
        {
            Vector3 directionToPlayer = player.position - monsterFace.position;
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
            monsterFace.rotation = rotationToPlayer;
        }
        else
        {
            Vector3 directionToPlayer = player.position - monster.transform.position;
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
            monster.transform.rotation = rotationToPlayer;
        }
    }

    // 스크립트가 활성화될 때 호출되는 메서드 (추가된 부분)
    void OnEnable()
    {
        isSpawning = true;
    }

    // 스크립트가 비활성화될 때 호출되는 메서드 (추가된 부분)
    void OnDisable()
    {
        isSpawning = false;
    }
}
