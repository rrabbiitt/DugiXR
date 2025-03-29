using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireman_L : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ��� ������ ������ �߰��մϴ�.
    public GameObject fireEffectPrefab; // �� ����Ʈ �������� �����ؾ� �մϴ�.

    private void Update()
    {
        // Fireman�� z+ �������� �̵��մϴ�.
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // ���� Fireman�� y ��ǥ�� 0 ���Ϸ� �������� ǳ�� �Ҹ��� ����ϰ� �ı��մϴ�.
        if (transform.position.y <= 22)
        {
            //PlaySplashSound();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� Fireman�� Log �ݶ��̴��� �浹�ϸ� �� ����Ʈ�� �����ϰ� �ı��մϴ�.
        if (other.CompareTag("Log"))
        {
            SpawnFireEffect();

            Timer timerScript = FindObjectOfType<Timer>();
            if (timerScript != null)
            {
                timerScript.UpdateScore();
            }

            Destroy(gameObject);
        }
    }

    private void SpawnFireEffect()
    {
        // �� ����Ʈ�� �����մϴ�.
        if (fireEffectPrefab != null)
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}
