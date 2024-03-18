using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedMove : MonoBehaviour
{

    public float amplitude = 1f;    // �������� ����
    public float frequency = 1f;    // �������� ���ļ�

    private Vector3 initialPosition; // �ʱ� ��ġ

    void Start()
    {
        // �ʱ� ��ġ ����
        initialPosition = transform.position;
    }

    void Update()
    {
        // �ð��� ���� y ���� ���� ��� (���� �Լ� ���)
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        // ���ο� ��ġ ����Ͽ� ����
        Vector3 newPosition = initialPosition + Vector3.up * yOffset;
        transform.position = newPosition;
    }

}
