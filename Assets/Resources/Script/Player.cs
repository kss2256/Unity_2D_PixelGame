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
    

    //�ʱ�ȭ
    private void Awake()
    {
        DontDestroyOnLoad(this);  
        mSpriteRenderer = GetComponent<SpriteRenderer>(); 
        mRigidbody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        
     

    }

    //���� üũ
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
                Debug.Log("���� �ϴ� ����.");           
                mSituation = Situation.FALL;
                mAnimator.SetTrigger("Fall");
            }            
            
            //����ĳ��Ʈ Ž���ؼ� ������� ������
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.6f)
                {
                    mIsJump = false;
                    mAnimator.SetTrigger("Ground");
                    Debug.Log("���� �����ִ�.");
                }
            }
        }

        
    }
    //�̵� ����
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

    //������ �۾�
    private void LateUpdate()
    {
        //�÷��̾� ���� ��ȯ
        //mInputVec.y = 0;
        if (mInputVec.x != 0)
        {
            //trut�� ����
            mSpriteRenderer.flipX = mInputVec.x < 0;         
        }

        if (mAnimator.runtimeAnimatorController)
        {
            //Run �ִϸ��̼� ���
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
