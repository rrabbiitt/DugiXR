using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave3 : MonoBehaviour
{
    public LoadData loadData;
    public CompleteGameData completeGameData;

    public GameObject monster01Prefab;
    public GameObject monster02Prefab;
    public GameObject apple;
    public GameObject bomb;
    public GameObject victoryCanvas;

    public AudioClip spawnSound;
    public AudioClip passSound;
    private AudioSource audioSource;
    private Transform player;
    private bool isSpawning = true;
    private int monsterNum;

    void Start()
    {
        completeGameData = FindObjectOfType<CompleteGameData>();
        loadData = FindObjectOfType<LoadData>();

        audioSource = gameObject.AddComponent<AudioSource>();
        monsterNum = 1;

        Invoke("SpawnOnce_M", 3f);
        InvokeRepeating("Spawn_M", 7f, 4f);
        Invoke("SpawnOnce_I", 9f);
        InvokeRepeating("Spawn_I", 18f, 9f);
        Invoke("DisableScript", 30f);
        player = Camera.main.transform;
    }

    void Update()
    {
        if (monsterNum == 0)
        {
            audioSource.PlayOneShot(passSound);
            victoryCanvas.SetActive(true);

            completeGameData.IsTuhoClear = true;
            loadData.NextScene = "Town";
            loadData.IsClear = true;
            loadData.PreviousScene = "Tuho";

            Bomb[] bombs = FindObjectsOfType<Bomb>();
            foreach (Bomb bomb in bombs)
            {
                Destroy(bomb.gameObject);
            }

            Apple[] apples = FindObjectsOfType<Apple>();
            foreach (Apple apple in apples)
            {
                Destroy(apple.gameObject);
            }

            monsterNum = -1;

            enabled = false;
        }
    }

    void SpawnOnce_M()
    {
        Spawn_M();
    }

    void SpawnOnce_I()
    {
        Spawn_I();
    }

    void Spawn_M()
    {
        if (!isSpawning) return;

        GameObject selectedMonsterPrefab = Random.Range(0, 2) == 0 ? monster01Prefab : monster02Prefab;

        Vector3 spawnPosition = new Vector3(Random.Range(500f, 506f), 10f, Random.Range(543f, 560f));

        GameObject monster = Instantiate(selectedMonsterPrefab, spawnPosition, Quaternion.identity);
        monsterNum++;
        AudioSource.PlayClipAtPoint(spawnSound, spawnPosition);

        LookAtCamera(monster);
    }

    void Spawn_I()
    {
        if (!isSpawning) return;

        GameObject selectedItemPrefab = Random.Range(0, 2) == 0 ? apple : bomb;

        Vector3 spawnPosition = new Vector3(Random.Range(502f, 512f), 10f, Random.Range(543f, 563f));

        GameObject item = Instantiate(selectedItemPrefab, spawnPosition, Quaternion.identity);
        //AudioSource.PlayClipAtPoint(spawnSound, spawnPosition);

        LookAtCamera(item);
    }

    void LookAtCamera(GameObject obj)
    {
        Vector3 directionToPlayer = player.position - obj.transform.position;
        Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
        obj.transform.rotation = rotationToPlayer;
    }

    public void MonsterDie()
    {
        monsterNum--;
    }

    void DisableScript()
    {
        monsterNum--;
        isSpawning = false;
    }
}
