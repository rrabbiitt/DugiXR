using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BowStringController : MonoBehaviour
{
    [SerializeField]
    private BowString bowStringRenderer;

    private bool isGrab;

    [SerializeField]
    private Transform midPointGrabObject;
    [SerializeField]
    private Transform midPointObject;

    [SerializeField]
    private float bowStringStretchLimit = 0.3f;
    
    public ArrowController arrowController;

    public float strength;

    // Start is called before the first frame update
    void Start()
    {
        isGrab = false;
    }

    private void Update()
    {
        if (isGrab)
        {
            bowStringRenderer.CreateString(midPointGrabObject.transform.position);
        }
    }

    public void GrabOn()
    {
        isGrab = true;
        PrepareBowString();
    }
    public void GrabOff()
    {
        isGrab = false;
        ResetBowString();
    }

    private void ResetBowString()
    {
        midPointObject.localPosition = new Vector3(-0.114f, 0, 0);
        arrowController.ReleaseArrow(strength);
        bowStringRenderer.CreateString(null);
    }

    private void PrepareBowString()
    {
        arrowController.prepareArrow();
        //bowStringRenderer.CreateString(midPointGrabObject.transform.position);
    }

}
