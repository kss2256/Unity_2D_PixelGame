using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallMove : Enemy
{
    [SerializeField]
    private float mRadius = 1.0f;
    [SerializeField]
    private float mAngleSpeed = 1.0f;

    private Vector3 mPos;
    [SerializeField]
    private const float mMaxAngle = 90.0f;
    
    public float Radius
    {
        get { return mRadius; }
        set { mRadius = value; }
    }

    public float AngleSpeed
    {
        get { return mAngleSpeed; }
        set { mAngleSpeed = value; }
    }



    private void Awake()
    {
        mPos = transform.position;
        //Radius Offeset
        mPos.y += mRadius;
    }

    private void Update()
    {

        
        float angle = Mathf.Sin(Time.time * mAngleSpeed) * mMaxAngle;

        // 각도를 이용하여 위치 계산
        Vector3 pos = Vector3.zero;
        pos.x = mRadius * Mathf.Sin(Mathf.Deg2Rad * angle);
        pos.y = -mRadius * Mathf.Cos(Mathf.Deg2Rad * angle);

        transform.position = mPos + pos;

        

    }


}
