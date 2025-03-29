using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCalculator : MonoBehaviour
{
    public float speed1 = 1.0f; // 1단계 속도 임계값
    public float speed2 = 2.0f; // 2단계 속도 임계값
    public float speed3 = 3.0f; // 3단계 속도 임계값
    public float speed4 = 6.0f;

    public int speedLevel = 0;

    private Vector3 previousPosition;

    private bool isCalcu;

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        // 오브젝트의 현재 속도를 계산
        Vector3 currentPosition = transform.position;
        float speed = Vector3.Distance(currentPosition, previousPosition) / Time.deltaTime;

        // 속도에 따라 단계를 출력
        speedLevel = CalculateSpeedLevel(speed);
        //Debug.Log("Speed Level: " + speedLevel);

        // 이전 위치 업데이트
        previousPosition = currentPosition;
    }

    private int CalculateSpeedLevel(float speed)
    {
        if (speed < speed1)
        {
            return 0; // 0단계: 움직임이 없음
        }
        else if (speed < speed2)
        {
            return 1; // 1단계: 조금 움직임이 있음
        }
        else if (speed < speed3)
        {
            return 2; // 2단계: 조금 더 빠르게 움직임
        }
        else if (speed < speed4)
        {
            return 3; // 3단계: 2단계보다 더 빠르게 움직임
        }
        else
        {
            return 4;
        }
    }

    public int GetSpeedLevel()
    {
        return speedLevel;
    }

    
}
