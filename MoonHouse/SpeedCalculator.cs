using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCalculator : MonoBehaviour
{
    public float speed1 = 1.0f; // 1�ܰ� �ӵ� �Ӱ谪
    public float speed2 = 2.0f; // 2�ܰ� �ӵ� �Ӱ谪
    public float speed3 = 3.0f; // 3�ܰ� �ӵ� �Ӱ谪
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
        // ������Ʈ�� ���� �ӵ��� ���
        Vector3 currentPosition = transform.position;
        float speed = Vector3.Distance(currentPosition, previousPosition) / Time.deltaTime;

        // �ӵ��� ���� �ܰ踦 ���
        speedLevel = CalculateSpeedLevel(speed);
        //Debug.Log("Speed Level: " + speedLevel);

        // ���� ��ġ ������Ʈ
        previousPosition = currentPosition;
    }

    private int CalculateSpeedLevel(float speed)
    {
        if (speed < speed1)
        {
            return 0; // 0�ܰ�: �������� ����
        }
        else if (speed < speed2)
        {
            return 1; // 1�ܰ�: ���� �������� ����
        }
        else if (speed < speed3)
        {
            return 2; // 2�ܰ�: ���� �� ������ ������
        }
        else if (speed < speed4)
        {
            return 3; // 3�ܰ�: 2�ܰ躸�� �� ������ ������
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
