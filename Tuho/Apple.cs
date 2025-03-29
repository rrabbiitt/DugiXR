using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public float speed = 0.2f;
    private Vector3 initialPosition;

    public AudioClip popSound;
    private AudioSource audioSource;

    private Transform mainCamera;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        float randomYOffset = Random.Range(-0.1f, 0.1f);
        initialPosition = new Vector3(transform.position.x, randomYOffset, transform.position.z);

        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        if (mainCamera != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, mainCamera.position, step);
        }

        // ī�޶� ��������� �ı�
        if (Vector3.Distance(transform.position, mainCamera.position) <= 0.3f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
            // ��� �ڽ� ������Ʈ ��������
            Transform[] childTransforms = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                childTransforms[i] = transform.GetChild(i);
            }

            // ��� �ڽ� ������Ʈ �ı�
            foreach (Transform child in childTransforms)
            {
                Destroy(child.gameObject);
                yield return null; // ���� �ڽ� �ı� ���� �� ������ ��ٸ���
            }

            if (popSound != null)
            {
                AudioSource.PlayClipAtPoint(popSound, transform.position);
            }

            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.IncreaseLives();
            }

            Destroy(gameObject);
        
    }
}
