using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float amplitude = 0.05f; // 위 아래로 움직이는 정도
    public float speed = 10f; // 앞으로 오는 속도
    private Vector3 initialPosition; // 처음 소환 자리
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

        // 초기 위치를 -0.1f에서 0.1f 사이의 값으로 설정
        float randomYOffset = Random.Range(-0.1f, 0.1f);
        initialPosition = new Vector3(transform.position.x, transform.position.y + randomYOffset, transform.position.z); // y 좌표를 초기 위치로 설정
        startTime = Time.time;

        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        if (mainCamera != null)
        {
            float yOffset = amplitude * Mathf.Sin((Time.time - startTime) * speed);
            float step = speed * Time.deltaTime * 10;

            // 카메라 쪽으로 이동하면서 y 방향으로 떠다니는 움직임 추가
            transform.position = Vector3.MoveTowards(transform.position, mainCamera.position, step);
            transform.position = new Vector3(transform.position.x, initialPosition.y + yOffset, transform.position.z); // y 좌표를 초기 y 좌표에 더하여 적용
        }

        // 카메라로부터 2 이내의 거리에 들어오면 파괴
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

        // 모든 자식 오브젝트 가져오기
        Transform[] childTransforms = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childTransforms[i] = transform.GetChild(i);
        }

        // 모든 자식 오브젝트 파괴
        foreach (Transform child in childTransforms)
        {
            Destroy(child.gameObject);
            yield return null; // 다음 자식 파괴 전에 한 프레임 기다리기
        }

        // 폭발 효과 생성
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 폭발 효과를 1초 동안 보여줌
        yield return new WaitForSeconds(1f);

        // 폭발 효과 제거
        Destroy(explosion);

        // 몬스터 제거
        Destroy(gameObject);

        // 점수 증가
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

        // 플레이어의 목숨 감소
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.DecreaseLives();
        }
    }
}
