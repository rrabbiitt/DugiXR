using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VerticalBillvoard : MonoBehaviour
{
    public Transform Player;
    public bool isYSet = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition;
        if (isYSet)
        {
            targetPosition = new Vector3(Player.position.x, Player.position.y, Player.position.z);
        }
        else
        {
            targetPosition = new Vector3(Player.position.x, transform.position.y, Player.position.z);
        }

        transform.LookAt(targetPosition);
    }
}
