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

        // 새로운 위치 설정 (x, y 좌표는 유지하고 z 좌표만 변경)
        Vector3 targetPosition = new Vector3(transform.position.x, 36.50485f, transform.position.z);
        float moveTime = 0.5f; // 이동에 걸리는 시간

        // 이동 애니메이션
        float elapsedTime = 0;
        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        // 5초 대기
        yield return new WaitForSeconds(20f);

        // 초기 위치로 되돌아감
        float returnTime = 0.5f; // 복귀에 걸리는 시간
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
