using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster02 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float yRange = 3f;
    public float destroyDistance = 10f;

    private Vector3 initialPosition;
    private float startY;
    private bool movingUp = true;
    private Transform mainCamera;

    public GameObject explosionPrefab;
    public AudioClip explosionSound;

    public AudioClip hurtSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        initialPosition = transform.position;
        startY = transform.position.y;
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        // ���� ī�޶��� ��ġ�� ������
        Vector3 cameraPosition = mainCamera.position;

        // ī�޶��� x�� z ��ǥ�� ������Ʈ�� ��ġ�� ����
        cameraPosition.y = transform.position.y;

        // ������Ʈ�� ī�޶� ��ġ�� �̵���Ŵ
        transform.position = Vector3.MoveTowards(transform.position, cameraPosition, moveSpeed * Time.deltaTime);

        // y�� �̵� ���
        float newY = transform.position.y + (movingUp ? 1 : -1) * moveSpeed * Time.deltaTime;
        if (Mathf.Abs(newY - startY) >= yRange)
        {
            movingUp = !movingUp;
        }

        // ������Ʈ ��ġ ����
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // ī�޶�� ���� �Ÿ� �̳��� �ִ� ��� �ı�
        if (Vector3.Distance(transform.position, mainCamera.position) <= destroyDistance)
        {
            if (hurtSound != null)
            {
                AudioSource.PlayClipAtPoint(hurtSound, transform.position);
            }
            DestroyMonsterAndDecreaseLives();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            StartCoroutine(ShowExplosionAndDestroy());
        }
    }

    IEnumerator ShowExplosionAndDestroy()
    {
        // ���� ȿ�� ���
        if (hurtSound != null)
        {
            AudioSource.PlayClipAtPoint(hurtSound, transform.position);
        }

        // ���� ȿ�� ���� �� ǥ��
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);

        // ���� ȿ�� ����
        Destroy(explosion);

        // ���� ����
        Destroy(gameObject);
    }

    void DestroyMonsterAndDecreaseLives()
    {
        // ���� ����
        Destroy(gameObject);

        // �÷��̾� ��� ����
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.DecreaseLives();
        }
    }
}
