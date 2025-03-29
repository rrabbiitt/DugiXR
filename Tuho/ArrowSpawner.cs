using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab; // ��ȣ ������
    public Transform arrowBox; // ��ȣ�� ������ �����̳� ��ġ
    public int numberOfArrows = 5; // ������ ��ȣ ����


    private void SpawnArrow()
    {
        Vector3 arrowBoxCenter = arrowBox.position;
        GameObject arrow = Instantiate(arrowPrefab, arrowBoxCenter, Quaternion.Euler(180, 180, 0));
        arrow.transform.parent = transform;
    }


    private void DestroyAllArrows()
    {
        // ��ȣ �뿡 �ִ� ��� ��ȣ ����
        foreach (Transform child in arrowBox)
        {
            Destroy(child.gameObject);
        }
    }

    // ��ȣ�� �ı��Ǹ� ȣ��Ǵ� �޼���
    public void OnArrowDestroyed()
    {
        // �� ��ȣ ����
        SpawnArrow();
    }
}
