using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheakPoint : MonoBehaviour
{

    private Animator mAnimator;
    private bool        mEnd;


    private void Awake()
    {
        mAnimator = GetComponent<Animator>();


    }


    private void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(mEnd == false)
            {
                mEnd = true;
                mAnimator.Play("Hit");
            }
        }

    }


}
