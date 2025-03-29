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
        // 메인 카메라의 위치를 가져옴
        Vector3 cameraPosition = mainCamera.position;

        // 카메라의 x와 z 좌표를 오브젝트의 위치에 적용
        cameraPosition.y = transform.position.y;

        // 오브젝트를 카메라 위치로 이동시킴
        transform.position = Vector3.MoveTowards(transform.position, cameraPosition, moveSpeed * Time.deltaTime);

        // y축 이동 계산
        float newY = transform.position.y + (movingUp ? 1 : -1) * moveSpeed * Time.deltaTime;
        if (Mathf.Abs(newY - startY) >= yRange)
        {
            movingUp = !movingUp;
        }

        // 오브젝트 위치 갱신
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // 카메라와 일정 거리 이내에 있는 경우 파괴
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
        // 폭발 효과 재생
        if (hurtSound != null)
        {
            AudioSource.PlayClipAtPoint(hurtSound, transform.position);
        }

        // 폭발 효과 생성 및 표시
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);

        // 폭발 효과 제거
        Destroy(explosion);

        // 몬스터 제거
        Destroy(gameObject);
    }

    void DestroyMonsterAndDecreaseLives()
    {
        // 몬스터 제거
        Destroy(gameObject);

        // 플레이어 목숨 감소
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.DecreaseLives();
        }
    }
}
