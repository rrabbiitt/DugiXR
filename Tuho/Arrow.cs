using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class Arrow : MonoBehaviour
{
    public float destroyHeight = 1f; // Arrow가 이 높이 아래로 내려가면 자동으로 파괴

    private void Update()
    {
        // Arrow의 현재 y 좌표가 destroyHeight 아래로 내려가면 파괴
        if (transform.position.y <= destroyHeight)
        {
            DestroyArrow();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 만약 충돌한 오브젝트가 "monster" 또는 "item" 태그를 가지고 있다면 Arrow를 파괴
        if (collision.gameObject.CompareTag("monster") || collision.gameObject.CompareTag("item") || collision.gameObject.CompareTag("Floor"))
        {
            DestroyArrow();
        }
    }

    private void DestroyArrow()
    {
        // Arrow가 파괴되면 TohoSpawner 스크립트의 OnTohoDestroyed 메서드 호출
        ArrowSpawner arrowSpawner = FindObjectOfType<ArrowSpawner>();
        if (arrowSpawner != null)
        {
            arrowSpawner.OnArrowDestroyed();
        }

        // Arrow 파괴
        Destroy(gameObject);
    }
}
