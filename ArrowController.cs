using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField]
    private GameObject midPointVisual, arrowPrefab, arrowSpawnPoint, arrowRotate;

    [SerializeField]
    private float arrowMaxSpeed = 10;

    public void prepareArrow()
    {
        midPointVisual.SetActive(true);
    }
    public void ReleaseArrow(float strength)
    {
        midPointVisual.SetActive(false);

        Debug.Log("zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz");

        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = arrowSpawnPoint.transform.position;
        arrow.transform.rotation = arrowRotate.transform.rotation;
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(arrow.transform.forward * strength * arrowMaxSpeed, ForceMode.Impulse);
    }
}
