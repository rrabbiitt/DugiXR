using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliff_R : MonoBehaviour
{
    [SerializeField]
    private AudioClip LiftSound;
    private AudioSource LiftSource;

    private Vector3 originalPosition;
    private bool isMoving = false;

    void Start()
    {
        originalPosition = transform.position;

        LiftSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("can"))
        {
            if (!isMoving)
            {
                LiftSource.PlayOneShot(LiftSound);
                StartCoroutine(MoveCliff());
            }
        }
    }

    IEnumerator MoveCliff()
    {
        isMoving = true;

        // ���ο� ��ġ ���� (x, y ��ǥ�� �����ϰ� z ��ǥ�� ����)
        Vector3 targetPosition = new Vector3(transform.position.x, 36.50485f, transform.position.z);
        float moveTime = 0.5f; // �̵��� �ɸ��� �ð�

        // �̵� �ִϸ��̼�
        float elapsedTime = 0;
        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        // 5�� ���
        yield return new WaitForSeconds(20f);

        // �ʱ� ��ġ�� �ǵ��ư�
        float returnTime = 0.5f; // ���Ϳ� �ɸ��� �ð�
        elapsedTime = 0;
        while (elapsedTime < returnTime)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, elapsedTime / returnTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;

        isMoving = false;
    }
}
