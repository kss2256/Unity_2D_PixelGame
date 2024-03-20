using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player;
//using static UnityEditor.PlayerSettings;
//using static UnityEngine.RuleTile.TilingRuleOutput;
using static SettingMenu;



public class Player : MonoBehaviour
{

    public enum Situation
    {
        IDLE,
        RUN,
        JUMP,
        DOUBLE_JUMP,
        WALL_JUMP,
        FALL,
        HIT,
    }

    public Vector2      mInputVec;
    public float        mSpeed;
    public float        mJumpForce;
    public bool         mIsJump;
    public int          mHp;

    public Situation    mSituation = Situation.IDLE;


    private SpriteRenderer mSpriteRenderer;
    private Rigidbody2D mRigidbody;
    private Animator mAnimator;
    

    //초기화
    private void Awake()
    {
        DontDestroyOnLoad(this);  
        mSpriteRenderer = GetComponent<SpriteRenderer>(); 
        mRigidbody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        
     

    }

    //상태 체크
    private void Update()
    {
        
       

        if (Input.GetKeyDown(KeyCode.Space) && !mIsJump)
        {
           
            if (mSituation == Situation.JUMP || mSituation == Situation.FALL)
            {
                Engine.mInstance.mAudioMgr.PlaySfx(AudioMgr.SfxType.JUMP);
                mIsJump = true;
                mRigidbody.velocity = Vector2.zero;
                mSituation = Situation.DOUBLE_JUMP;
                mRigidbody.AddForce(Vector2.up * mJumpForce , ForceMode2D.Impulse);
                mAnimator.SetTrigger("Double_Jump");
            }
            else
            {
                Engine.mInstance.mAudioMgr.PlaySfx(AudioMgr.SfxType.JUMP);
                mSituation = Situation.JUMP;
                mRigidbody.AddForce(Vector2.up * mJumpForce, ForceMode2D.Impulse);
                mAnimator.SetTrigger("Jump");
            }
        }

        RaycastHit2D rayHit = Physics2D.Raycast(mRigidbody.position, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (mRigidbody.velocity.y < 0)
        {
            if(!(mSituation == Situation.HIT))
            {
                Debug.Log("낙하 하는 도중.");           
                mSituation = Situation.FALL;
                mAnimator.SetTrigger("Fall");
            }            
            
            //레이캐스트 탐색해서 결과물이 나오면
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.6f)
                {
                    mIsJump = false;
                    mAnimator.SetTrigger("Ground");
                    Debug.Log("땅에 땋아있다.");
                }
            }
        }

        
    }
    //이동 로직
    private void FixedUpdate()
    {
        Vector3 newVec = mInputVec * mSpeed * Time.fixedDeltaTime;
        newVec.y = 0;
        if (mSituation == Situation.RUN)
        {            
            Engine.mInstance.mAudioMgr.PlaySfx(AudioMgr.SfxType.WALK);
            transform.position += newVec;
        }
        else
            transform.position += newVec * 0.75f;

        // transform.Translate(newVec);
        Debug.DrawRay(mRigidbody.position, Vector3.down, Color.green);


    }

    //마지막 작업
    private void LateUpdate()
    {
        //플레이어 방향 전환
        //mInputVec.y = 0;
        if (mInputVec.x != 0)
        {
            //trut가 왼쪽
            mSpriteRenderer.flipX = mInputVec.x < 0;         
        }

        if (mAnimator.runtimeAnimatorController)
        {
            //Run 애니메이션 재생
            //mAnimator.SetFloat("Speed", mInputVec.magnitude);
            mAnimator.SetFloat("Speed", Mathf.Abs(mInputVec.x));

        }


    }



    private void OnMove(InputValue _vluae)
    {
        mInputVec = _vluae.Get<Vector2>();
    }

   

    private void Death()
    {
        gameObject.SetActive(false);
    }
    private void Idle()
    {
        mSituation = Situation.IDLE;       
    }
    private void Run()
    {
        mSituation = Situation.RUN;
    }

    public void Hit()
    {
        if (mSituation == Situation.HIT)
            return;
        mSituation = Situation.HIT;
        mAnimator.SetTrigger("Hit");
    }

   


    


}
