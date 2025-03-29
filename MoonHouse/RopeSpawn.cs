using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    public GameObject partPrefab, parentObject;

    [Range(1, 1000)]
    public int length = 1;

    public float partDistance = 0.21f;

    public bool reset, spawn, snapFirst, snapLast;

    void Start()
    {
       
    }

    void Update()
    {
        if (reset)
        {
            foreach(GameObject tmp in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(tmp);
            }

            reset = false;
        }

        if (spawn)
        {
            Spawn();

            spawn = false;
        }
    }

    public void Spawn()
    {
        int count = (int)(length / partDistance);
        for (int x = 0; x < count; x++)
        {
            GameObject tmp;

            tmp = Instantiate(partPrefab, new Vector3(transform.position.x, transform.position.y * partDistance * (x + 1), transform.position.z), Quaternion.identity, parentObject.transform);
            tmp.transform.eulerAngles = new Vector3(100, 0, 0);

            tmp.name = parentObject.transform.childCount.ToString();

            if (x == 0)
            {
                Destroy(tmp.GetComponent<CharacterJoint>());
            }
            else
            {
                tmp.GetComponent<CharacterJoint>().connectedBody = parentObject.transform.Find((parentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
    }
}


