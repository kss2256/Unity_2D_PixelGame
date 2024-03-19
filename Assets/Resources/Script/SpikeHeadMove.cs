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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            StartCoroutine(KnockBack(collision.gameObject));
        }

    }

    private IEnumerator KnockBack(GameObject _target)
    {
        Engine.mInstance.mPlayer.Hit();


        Vector3 playerPos = _target.transform.position;
        Vector3 dirVec = playerPos - transform.position;
        Rigidbody2D mRigidbody = _target.GetComponent<Rigidbody2D>();
        mRigidbody.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

        
        yield return mWaitFixedUpdate;

       
    }

}
