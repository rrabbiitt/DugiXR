using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed = 0.2f;
    private Vector3 initialPosition;

    private Transform mainCamera;

    void Start()
    {
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

        // 카메라 가까워지면 파괴
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

        Monster[] monsters = FindObjectsOfType<Monster>();
        foreach (Monster monster in monsters)
        {
            monster.BombExplosion();
        }

        Destroy(gameObject);
    }
}
