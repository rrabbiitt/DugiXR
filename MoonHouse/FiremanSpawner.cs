using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiremanSpawner : MonoBehaviour
{
    public GameObject firemanLPrefab; // FIREMAN_L �������� �����ؾ� �մϴ�.
    public GameObject firemanRPrefab; // FIREMAN_R �������� �����ؾ� �մϴ�.
    public Vector3 spawnPosition1; // ù ��° ��ȯ ��ġ
    public Vector3 spawnPosition2; // �� ��° ��ȯ ��ġ

    private float spawnTimer = 0f; // ��ȯ Ÿ�̸�
    private bool spawnAtPosition1 = true; // ù ��° ��ġ�� ��ȯ���� �� ��° ��ġ�� ��ȯ������ �����ϴ� �÷���

    private void Update()
    {
        // �� �����Ӹ��� ��ȯ Ÿ�̸Ӹ� ������ŵ�ϴ�.
        spawnTimer += Time.deltaTime;

        // ��ȯ Ÿ�̸Ӱ� 2�� �̻��� ������ Fireman�� ��ȯ�ϰ� Ÿ�̸Ӹ� �缳���մϴ�.
        if (spawnTimer >= 2f)
        {
            SpawnFireman();
            spawnTimer = 0f; // Ÿ�̸� �ʱ�ȭ
        }
    }

    private void SpawnFireman()
    {
        if (spawnAtPosition1)
        {
            if (firemanLPrefab != null)
            {
                Instantiate(firemanLPrefab, spawnPosition1, Quaternion.identity);
            }
        }
        else
        {
            if (firemanRPrefab != null)
            {
                // Fireman_R �������� ��ȯ�� �� 180�� ȸ���Ͽ� ��ȯ�մϴ�.
                GameObject fireman = Instantiate(firemanRPrefab, spawnPosition2, Quaternion.Euler(0f, 180f, 0f));
            }
        }

        spawnAtPosition1 = !spawnAtPosition1; // �÷��׸� ����Ͽ� ���� ��ȯ ��ġ�� �����մϴ�.
    }
}
