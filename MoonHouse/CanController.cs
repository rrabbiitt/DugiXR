using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanController : MonoBehaviour
{
    public ParticleSystem explosionPrefab;
    public AudioClip successSound;
    public AudioClip flyingSound;
    private AudioSource audioSource;

    public GameObject canPrefab;
    public Vector3 spawnPosition;

    public Transform Player;

    private float pushPower = 80f;
    private bool isGrab = false;
    private bool isRelease = false;

    public CanRotation rotationScript;
    public GameObject fireEffect;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        rotationScript.enabled = true;
        fireEffect.SetActive(false);
    }

    void Update()
    {
        if (transform.position.y <= 0)
        {
            DestroyCan();
        }

        if (isRelease)
        {
            rotationScript.enabled = false;
            fireEffect.SetActive(true);

            Vector3 moveDirection = this.transform.position - Player.position;
            moveDirection.Normalize();
            this.transform.Translate(moveDirection * pushPower * Time.deltaTime, Space.World);
        }
    }

    public void LaunchCan()
    {
        audioSource.PlayOneShot(flyingSound);
        isRelease = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cliff"))
        {
            AudioSource.PlayClipAtPoint(successSound, transform.position);
            ParticleSystem explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosionEffect.gameObject, 1f);

            DestroyCan();
        }

        if (other.CompareTag("Floor"))
        {
            DestroyCan();
        }
    }

    void DestroyCan()
    {
        GameObject newCan = Instantiate(canPrefab, spawnPosition, Quaternion.Euler(0f, 90f, 0f));

        Destroy(gameObject);
    }
}