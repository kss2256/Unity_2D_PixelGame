using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public delegate float FunctionAddTime();


    private Vector3 mTitlePixedPos;
    private Animator mAnimator;
    private SpriteRenderer mSpriteRenderer;
    private Material mMaterial;
    private const float mMinSpawnTime = -0.5f;
    private const float mMaxSpawnTime = 0.5f;
    private float mCurSpawnTime;
    private FunctionAddTime mFuncAddTime;
    private bool mbIsAppear = false;




    private void Awake()
    {       
        mTitlePixedPos = new Vector3(0.0f, -6.5f, 0.0f);
        mAnimator = GetComponent<Animator>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();

        mSpriteRenderer.material = (Material)Resources.Load("Material\\SpawnMaterial");
        mMaterial =  mSpriteRenderer.material;

    }

    private void Update()
    {
        
        if(mbIsAppear)
        {
            mCurSpawnTime += Time.deltaTime * mFuncAddTime() * 0.5f;
            if(mCurSpawnTime >= mMaxSpawnTime)
            {
                mbIsAppear = false;
                mCurSpawnTime = mMaxSpawnTime;
            }

            mMaterial.SetFloat("_SplitValue", mCurSpawnTime);
        }


    }


    public void Appearing()
    {
        //Animation Setting
        mAnimator.Play("Appearing");
    }
    public void Appear()
    {
        //Material Setting
        mCurSpawnTime = mMinSpawnTime;
        mFuncAddTime = Up;
        mbIsAppear = true;

    }

    public void Disappear()
    {
        //Material Setting
        mCurSpawnTime = mMaxSpawnTime;
        mFuncAddTime = Down;
        mbIsAppear = true;

    }

    public void MaksDude()
    {
        transform.position = mTitlePixedPos;
        mAnimator.runtimeAnimatorController =
                (RuntimeAnimatorController)Resources.Load
                ("Animation\\Mask\\Mask Dude");
        Appearing();
    }

    public void NinjaFrog()
    {
        transform.position = mTitlePixedPos;
        mAnimator.runtimeAnimatorController =
        (RuntimeAnimatorController)Resources.Load
        ("Animation\\Ninja\\Ninja Frog");
        Appearing();
    }
    public void PinkMan()
    {
        transform.position = mTitlePixedPos;
        mAnimator.runtimeAnimatorController =
        (RuntimeAnimatorController)Resources.Load
        ("Animation\\Pink\\Pink Man");
        Appearing();
    }

    public void VirtualGuy()
    {
        transform.position = mTitlePixedPos;
        mAnimator.runtimeAnimatorController =
        (RuntimeAnimatorController)Resources.Load
        ("Animation\\Virtual\\Virtual Guy");
        Appearing();
    }

    private float Up()
    {
        return 1.0f;
    }

    private float Down()
    {
        return -1.0f;
    }
}
