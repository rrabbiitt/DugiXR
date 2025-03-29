using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCenterOfMass : MonoBehaviour
{
    [SerializeField]
    private JegiStartInteraction jegiStartInteraction;

    [SerializeField]
    private Transform model;

    [SerializeField]
    private GameObject selfPrefabs;

    [SerializeField]
    private AudioSource triggerAudio;

    public JegiInteraction jegiInt;
    private Vector3 previousPosition;
    private bool isAttached;

    private Rigidbody rb;
    private float forceMagnitude = 10f;

    void Start()
    {

        jegiInt = GameObject.Find("JegiGameRuleManager").GetComponent<JegiInteraction>();
        triggerAudio = gameObject.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (!isAttached)
        {
            if (transform.position.y > previousPosition.y)
            {
                model.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            }
            else if (transform.position.y <= previousPosition.y)
            {
                model.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            }

            previousPosition = transform.position;
        }
        if (transform.position.x > 5 || transform.position.x < -5 ||
            transform.position.z > 5 || transform.position.z < -5 ||
            transform.position.y < -2)
        {
            GameSet();
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (jegiInt.isStart)
        {
            isAttached = true;
            if (collision.transform.tag == "Hand")
            {
                jegiInt.ShowStars();
                Vector3 groundNormal = collision.ClosestPoint(transform.position) - transform.position;
                Vector3 bounceDirection = Vector3.Reflect(Vector3.up, groundNormal).normalized;

                // 실제 활동량
                Vector3 finalBounceDirection = new Vector3(-bounceDirection.x, bounceDirection.y, -bounceDirection.z);
                // 테스트용
                //Vector3 finalBounceDirection = new Vector3(0, bounceDirection.y, 0);

                GetComponent<Rigidbody>().AddForce(finalBounceDirection * forceMagnitude, ForceMode.Impulse);
                StartCoroutine(EffectAndSound());
            }
            if (collision.transform.tag == "Destroy")
            {
                GameSet();
            }
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        isAttached = false;
    }

    public void GameSet()
    {
        isAttached = false;
        jegiInt.isStart = false;
        jegiInt.ResetStars();
        jegiStartInteraction.Retry.SetActive(true);
        jegiStartInteraction.Home.SetActive(true);
        rb.constraints = RigidbodyConstraints.FreezePosition;
        forceMagnitude = 10f;
        Instantiate(selfPrefabs, new Vector3(0, 0.15f, 0.581f), Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void IsGrab(bool check)
    {
        isAttached = check;
    }

    IEnumerator EffectAndSound()
    {
        triggerAudio.Play();
        yield return new WaitForSeconds(0.5f);
        triggerAudio.Stop();
    }
}
