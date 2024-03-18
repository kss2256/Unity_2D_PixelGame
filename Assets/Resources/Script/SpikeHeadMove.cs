using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadMove : MonoBehaviour
{

    [SerializeField]
    private float mAmplitude = 1.0f;
    [SerializeField]
    private float mFreQuency = 1.0f;

    private Vector3 mPos;


    private void Awake()
    {
        mPos = transform.position;
    }

    private void Update()
    {
        float movePosY = MathF.Sin(Time.time * mFreQuency) * mAmplitude;

        Vector3 pos = mPos + Vector3.up * movePosY;
        transform.position = pos;

        if(transform.position.y <= mPos.y - (mAmplitude - 0.01))
        Debug.Log("여기서 딱 그만");

    }



}
