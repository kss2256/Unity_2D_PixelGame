using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.U2D;
using UnityEngine;

public class SawMove : Enemy
{
    public enum Direction
    {
        NONE,
        RIGHT,
        UP,
        LEFT,
        DOWN,
        END,
    }

    public Vector2      mMatrix;
    public Vector3      mCellSize;
    public Sprite       mSprite;
    public float        mSpeed = 1.0f;
    public float        mWaitTime = 2.0f;
    public bool         mAlpha;    
    public Direction    mDir;
    public float        mStopTime = 3.0f;

    private Vector3 mFixedPos;
    private Direction mFixdeDir;
    private int     mCount;
    private GameObject mSawParent;
    private Animator mAnimator;
    //private Rigidbody2D mRigidbody;

    private WaitForFixedUpdate mWaitFixedUpdate;

    private void Awake()
    {
        mWaitFixedUpdate = new WaitForFixedUpdate();
        mFixedPos = transform.position;
        mFixdeDir = mDir;
        mAnimator = GetComponent<Animator>();
        //mRigidbody = GetComponent<Rigidbody2D>();
        Vector3 pos = Vector3.zero;
        mSawParent = new GameObject();
        mSawParent.name = name + "Chain";
        for (int i = 0; i < mMatrix.x ; i++)
        {
            pos.x = (i * mCellSize.x);
            for (int j = 0; j < mMatrix.y ; j++)
            {     
                pos.y = (j * mCellSize.y);
                if (i == 0 || i == mMatrix.x - 1 || j == 0 || j == mMatrix.y - 1)
                {
                    GameObject Saw = new GameObject();
                    Saw.name = (i + 1).ToString() + "ї­" + (j + 1).ToString() + "За";
                    Saw.transform.position = pos + transform.position;
                    Saw.AddComponent<SpriteRenderer>().sprite = mSprite;
                    Saw.transform.parent = mSawParent.transform;
                    if(!mAlpha)
                    {
                        Saw.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    }
                } 
            }
        }
        switch (mDir)
        {
            case Direction.RIGHT:
                transform.position = mFixedPos;
                break;
            case Direction.UP:
                {
                    Vector3 nextpos = transform.position;
                    nextpos.x = mFixedPos.x + ((mMatrix.x * mCellSize.x) - mCellSize.x);
                    transform.position = nextpos;
                }
                break;
            case Direction.LEFT:
                {
                    Vector3 nextpos = transform.position;
                    nextpos.x = mFixedPos.x + ((mMatrix.x * mCellSize.x) - mCellSize.x);
                    nextpos.y = mFixedPos.y + ((mMatrix.y * mCellSize.y) - mCellSize.y);
                    transform.position = nextpos;
                }
                break;
            case Direction.DOWN:
                {
                    Vector3 nextpos = transform.position;
                    nextpos.y = mFixedPos.y + ((mMatrix.y * mCellSize.y) - mCellSize.y);
                    transform.position = nextpos;
                }
                break;
        }

    }

    private void Update()
    {

 
    }

    private void FixedUpdate()
    {
        

            switch (mDir)
            {
                case Direction.RIGHT:
                    {
                        transform.Translate(Vector3.right * mSpeed * Time.fixedDeltaTime);
                        if (transform.position.x >= mFixedPos.x + ((mMatrix.x * mCellSize.x) - mCellSize.x))
                        {
                            Vector3 pos = transform.position;
                            pos.x = mFixedPos.x + ((mMatrix.x * mCellSize.x) - mCellSize.x);
                            transform.position = pos;
                            mCount++;
                            if (mCount == 4)
                            {
                                mDir = Direction.END;
                                mAnimator.Play("Idle");
                            }
                            else
                                mDir = Direction.UP;
                        }
                    }
                    break;
                case Direction.UP:
                    {
                        transform.Translate(Vector3.up * mSpeed * Time.fixedDeltaTime);
                        if (transform.position.y >= mFixedPos.y + ((mMatrix.y * mCellSize.y) - mCellSize.x))
                        {
                            Vector3 pos = transform.position;
                            pos.y = mFixedPos.y + ((mMatrix.y * mCellSize.y) - mCellSize.y);
                            transform.position = pos;
                            mCount++;
                            if (mCount == 4)
                            {
                                mDir = Direction.END;
                                mAnimator.Play("Idle");
                            }
                            else
                                mDir = Direction.LEFT;
                        }
                    }
                    break;
                case Direction.LEFT:
                    {
                        transform.Translate(Vector3.left * mSpeed * Time.fixedDeltaTime);
                        if (transform.position.x <= mFixedPos.x)
                        {
                            Vector3 pos = transform.position;
                            pos.x = mFixedPos.x;
                            transform.position = pos;
                            mCount++;
                            if (mCount == 4)
                            {
                                mDir = Direction.END;
                                mAnimator.Play("Idle");
                            }
                            else
                                mDir = Direction.DOWN;
                        }
                    }
                    break;
                case Direction.DOWN:
                    {
                        transform.Translate(Vector3.down * mSpeed * Time.fixedDeltaTime);
                        if (transform.position.y <= mFixedPos.y)
                        {
                            Vector3 pos = transform.position;
                            pos.y = mFixedPos.y;
                            transform.position = pos;

                            mCount++;
                            if (mCount == 4)
                            {
                                mDir = Direction.END;
                                mAnimator.Play("Idle");
                            }
                            else
                                mDir = Direction.RIGHT;
                        }
                    }
                    break;
                case Direction.END:
                    {
                        StartCoroutine(SawAction());
                        mDir = Direction.NONE;
                    }
                    break;
                default:
                    break;
           


        }
    }


    

    private IEnumerator SawAction()
    {
        yield return new WaitForSeconds(mStopTime);

        mAnimator.Play("Play");
        mDir = mFixdeDir;
        mCount = 0;
    }

    


}
