using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanRotation : MonoBehaviour
{
    public float rotationSpeed = 0f; // ȸ�� �ӵ�
    public float rotationSpeedChangeSpeed = 5f;
    public Transform centerObject;    // �߽����� ��Ÿ���� ���� ������Ʈ�� Transform

    public ParticleSystem explosionPrefab;
    public AudioClip explosionSound;

    public GameObject Fire01Prefab;
    public GameObject Fire02Prefab;
    public GameObject Fire03Prefab;

    public int speedLevel = 0;

    void Update()
    {
        SpeedCalculator speedCalculator = FindObjectOfType<SpeedCalculator>();

        if (speedCalculator != null)
        {
            // SpeedCalculator���� ���� speedLevel �� ��������
            speedLevel = speedCalculator.GetSpeedLevel();

            // SpeedCalculator���� ���� speedLevel �� ��������
            int newSpeedLevel = speedCalculator.GetSpeedLevel();

            // speedLevel�� ���� rotationSpeed ���� (������ ����)
            float targetSpeed = GetSpeedForLevel(newSpeedLevel);
            rotationSpeed = Mathf.Lerp(rotationSpeed, targetSpeed, rotationSpeedChangeSpeed * Time.deltaTime);

            UpdateFirePrefabs(newSpeedLevel);

            //Debug.Log("rotationSpeed: " + rotationSpeed);

            if (newSpeedLevel == 4)
            {
                // ���� ����Ʈ ���
                PlayExplosionEffect();
                // 1�� �Ŀ� �ı� �� �����
                Invoke("DestroyAndRespawn", 1f);
            }
        }

        // �߽����� ����� ��ġ ���
        Vector3 pivot = centerObject.position;

        // z ���� �߽����� 360�� ȸ��
        transform.RotateAround(pivot, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    public int GetSpeedLevel()
    {
        return speedLevel;
    }

    void UpdateFirePrefabs(int level)
    {
        if (level >= 0 && level <= 1)
        {
            Fire01Prefab.SetActive(false);
            Fire02Prefab.SetActive(false);
            Fire03Prefab.SetActive(false);
        }
        else
        {
            Fire01Prefab.SetActive(level == 1 || level == 0);
            Fire02Prefab.SetActive(level == 2);
            Fire03Prefab.SetActive(level == 3);
        }

        // rotationspeed�� 0�̸� Fire01Prefab ��Ȱ��ȭ
        if (Mathf.Approximately(rotationSpeed, 0f))
        {
            Fire01Prefab.SetActive(false);
        }
    }

    float GetSpeedForLevel(int level)
    {
        switch (level)
        {
            case 0:
                return 0f;
            case 1:
                return 1000f;
            case 2:
                return 2000f;
            case 3:
                return 3000f;
            case 4:
                return 0f; // ���� ����Ʈ �� �ʱ�ȭ
            default:
                return 0f;
        }
    }

    void PlayExplosionEffect()
    {
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        // ���� ����Ʈ ���
        ParticleSystem explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 1�� �ڿ� ���� ����Ʈ �ı�
        Destroy(explosionEffect.gameObject, 1f);
    }

    void DestroyAndRespawn()
    {
        // CansSpawner ��ũ��Ʈ�� ���� ������Ʈ�� ã�Ƽ� SpawnCan �޼��� ȣ��
        CanSpawner canSpawner = FindObjectOfType<CanSpawner>();
        if (canSpawner != null)
        {
            canSpawner.SpawnCan();
        }

        // ���� ���� ������Ʈ �ı�
        Destroy(gameObject);
    }

    public void GameOverDestroy()
    {
        Destroy(gameObject);
    }
}
