using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject apple;
    public GameObject bomb;

    private Transform player;
    private Transform face;
    private bool isSpawning = true; // 몬스터 생성 여부를 제어하는 부울 변수

    void Start()
    {
        Invoke("SpawnOnce", 9f);
        InvokeRepeating("Spawn", 18f, 9f);
        player = Camera.main.transform;
    }

    void SpawnOnce()
    {
        Spawn();
    }

    void Spawn()
    {
        if (!isSpawning) return; // 몬스터 생성 여부 확인

        GameObject selectedItemPrefab = Random.Range(0, 2) == 0 ? apple : bomb;

        float randomX = Random.Range(-2f, 2f);
        float randomY = Random.Range(-0.5f, 0.5f);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 3f);

        GameObject item = Instantiate(selectedItemPrefab, spawnPosition, Quaternion.identity);
        //AudioSource.PlayClipAtPoint(spawnSound, spawnPosition);

        LookAtCamera(item);
    }

    void LookAtCamera(GameObject item)
    {
        Transform itemFace = item.transform.Find("ItemFace");
        if (itemFace != null)
        {
            Vector3 directionToPlayer = player.position - itemFace.position;
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
            itemFace.rotation = rotationToPlayer;
        }
        else
        {
            Vector3 directionToPlayer = player.position - item.transform.position;
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
            item.transform.rotation = rotationToPlayer;
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
