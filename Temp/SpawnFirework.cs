using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFirework : MonoBehaviour
{
    public GameObject FireworkPrefab;

    public GameObject[] Lines;
    // Start is called before the first frame update
    void Start()
    {
        SpawnSparkles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSparkles()
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            Instantiate(FireworkPrefab, Lines[i].transform.position + new Vector3(0,-1,0), Quaternion.Euler(90,0,0), Lines[i].transform);
        }
    }
}
