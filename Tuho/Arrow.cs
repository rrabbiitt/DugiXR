using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class Arrow : MonoBehaviour
{
    public float destroyHeight = 1f; // Arrow�� �� ���� �Ʒ��� �������� �ڵ����� �ı�

    private void Update()
    {
        // Arrow�� ���� y ��ǥ�� destroyHeight �Ʒ��� �������� �ı�
        if (transform.position.y <= destroyHeight)
        {
            DestroyArrow();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���� �浹�� ������Ʈ�� "monster" �Ǵ� "item" �±׸� ������ �ִٸ� Arrow�� �ı�
        if (collision.gameObject.CompareTag("monster") || collision.gameObject.CompareTag("item") || collision.gameObject.CompareTag("Floor"))
        {
            DestroyArrow();
        }
    }

    private void DestroyArrow()
    {
        // Arrow�� �ı��Ǹ� TohoSpawner ��ũ��Ʈ�� OnTohoDestroyed �޼��� ȣ��
        ArrowSpawner arrowSpawner = FindObjectOfType<ArrowSpawner>();
        if (arrowSpawner != null)
        {
            arrowSpawner.OnArrowDestroyed();
        }

        // Arrow �ı�
        Destroy(gameObject);
    }
}
