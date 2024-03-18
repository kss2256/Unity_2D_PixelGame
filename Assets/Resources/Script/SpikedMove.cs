using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedMove : MonoBehaviour
{

    public float amplitude = 1f;    // 움직임의 진폭
    public float frequency = 1f;    // 움직임의 주파수

    private Vector3 initialPosition; // 초기 위치

    void Start()
    {
        // 초기 위치 저장
        initialPosition = transform.position;
    }

    void Update()
    {
        // 시간에 따른 y 축의 변위 계산 (사인 함수 사용)
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        // 새로운 위치 계산하여 적용
        Vector3 newPosition = initialPosition + Vector3.up * yOffset;
        transform.position = newPosition;
    }

}
