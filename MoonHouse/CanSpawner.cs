using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSpawner : MonoBehaviour
{
    public GameObject canPrefab;
    public Vector3 spawnPosition;

    public void SpawnCan()
    {
        GameObject newCan = Instantiate(canPrefab, spawnPosition, Quaternion.Euler(0f, 90f, 0f));
    }
}
