using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float amplitude = 0.05f; // �� �Ʒ��� �����̴� ����
    public float speed = 10f; // ������ ���� �ӵ�
    private Vector3 initialPosition; // ó�� ��ȯ �ڸ�
    private float startTime;

    public GameObject explosionPrefab;
    public AudioClip explosionSound;
    public int scoreValue = 2;
    private Transform mainCamera;

    public AudioClip hurtSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        // �ʱ� ��ġ�� -0.1f���� 0.1f ������ ������ ����
        float randomYOffset = Random.Range(-0.1f, 0.1f);
        initialPosition = new Vector3(transform.position.x, transform.position.y + randomYOffset, transform.position.z); // y ��ǥ�� �ʱ� ��ġ�� ����
        startTime = Time.time;

        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        if (mainCamera != null)
        {
            float yOffset = amplitude * Mathf.Sin((Time.time - startTime) * speed);
            float step = speed * Time.deltaTime * 10;

            // ī�޶� ������ �̵��ϸ鼭 y �������� ���ٴϴ� ������ �߰�
            transform.position = Vector3.MoveTowards(transform.position, mainCamera.position, step);
            transform.position = new Vector3(transform.position.x, initialPosition.y + yOffset, transform.position.z); // y ��ǥ�� �ʱ� y ��ǥ�� ���Ͽ� ����
        }

        // ī�޶�κ��� 2 �̳��� �Ÿ��� ������ �ı�
        if (Vector3.Distance(transform.position, mainCamera.position) <= 2.0f)
        {
            Debug.Log("Check");
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

    public void BombExplosion()
    {
        StartCoroutine(ShowExplosionAndDestroy());
    }

    IEnumerator ShowExplosionAndDestroy()
    {
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

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

        // ���� ȿ�� ����
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // ���� ȿ���� 1�� ���� ������
        yield return new WaitForSeconds(1f);

        // ���� ȿ�� ����
        Destroy(explosion);

        // ���� ����
        Destroy(gameObject);

        // ���� ����
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.IncreaseScore(scoreValue);
        }

        Wave1 wave1 = FindObjectOfType<Wave1>();
        wave1.MonsterDie();
        Wave2 wave2 = FindObjectOfType<Wave2>();
        wave2.MonsterDie();
        Wave3 wave3 = FindObjectOfType<Wave3>();
        wave3.MonsterDie();
    }

    void DestroyMonsterAndDecreaseLives()
    {
        Wave1 wave1 = FindObjectOfType<Wave1>();
        wave1.MonsterDie();
        Wave2 wave2 = FindObjectOfType<Wave2>();
        wave2.MonsterDie();
        Wave3 wave3 = FindObjectOfType<Wave3>();
        wave3.MonsterDie();

        Destroy(gameObject);

        // �÷��̾��� ��� ����
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.DecreaseLives();
        }
    }
}
