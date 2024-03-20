using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadMove : Enemy
{

    [SerializeField]
    private float mAmplitude = 1.0f;
    [SerializeField]
    private float mFreQuency = 1.0f;

    private Vector3 mPos;
    private WaitForFixedUpdate mWaitFixedUpdate;


    private void Awake()
    {
        mWaitFixedUpdate = new WaitForFixedUpdate();
        mPos = transform.position;
    }

    private void FixedUpdate()
    {

        float movePosY = MathF.Sin(Time.time * mFreQuency) * mAmplitude;

        Vector3 pos = Vector3.up * movePosY;
        pos.y += -(mAmplitude);
        transform.position = mPos + pos;

        if (transform.position.y <= (mPos.y - mAmplitude) - (mAmplitude - 0.0001))
        {

            Debug.Log("여기서 딱 그만");
        }
       

    }



}
