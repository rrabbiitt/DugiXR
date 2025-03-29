using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab; // 투호 프리팹
    public Transform arrowBox; // 투호가 생성될 컨테이너 위치
    public int numberOfArrows = 5; // 생성할 투호 개수


    private void SpawnArrow()
    {
        Vector3 arrowBoxCenter = arrowBox.position;
        GameObject arrow = Instantiate(arrowPrefab, arrowBoxCenter, Quaternion.Euler(180, 180, 0));
        arrow.transform.parent = transform;
    }


    private void DestroyAllArrows()
    {
        // 투호 통에 있는 모든 투호 삭제
        foreach (Transform child in arrowBox)
        {
            Destroy(child.gameObject);
        }
    }

    // 투호가 파괴되면 호출되는 메서드
    public void OnArrowDestroyed()
    {
        // 새 투호 생성
        SpawnArrow();
    }
}
