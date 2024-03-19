using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallMove : MonoBehaviour
{
    [SerializeField]
    private float mRadius = 1.0f;
    [SerializeField]
    private float mAngularSpeed = 1.0f;

    private Vector3 mPos;
    private const float mMaxAngle = 90.0f;

    private void Awake()
    {
        mPos = transform.position;
        //Radius Offeset
        mPos.y += mRadius;
    }

    private void Update()
    {

        
        float angle = Mathf.Sin(Time.time * mAngularSpeed) * mMaxAngle;

        // 각도를 이용하여 위치 계산
        Vector3 pos = Vector3.zero;
        pos.x = mRadius * Mathf.Sin(Mathf.Deg2Rad * angle);
        pos.y = -mRadius * Mathf.Cos(Mathf.Deg2Rad * angle);

        transform.position = mPos + pos;

        
        



    }


}
