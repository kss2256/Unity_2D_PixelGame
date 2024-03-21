using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheakPoint : MonoBehaviour
{

    private Animator mAnimator;
    private bool        mEnd;
    private Vector3 mStage_2Pos = new Vector3(35.35f, -2.49f);


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
                Engine.mInstance.mAudioMgr.PlaySfx(AudioMgr.SfxType.END_POINT);

                mEnd = true;
                mAnimator.Play("Hit");
                if(Engine.mInstance.mSceneMgr.CurScene == SceneMgr.SceneType.STAGE_1)
                StartCoroutine(Stage_2());
                else if(Engine.mInstance.mSceneMgr.CurScene == SceneMgr.SceneType.STAGE_2)
                {
                    StartCoroutine(Win());
                    
                }
            }
        }
    }


    public IEnumerator Stage_2()
    {
        yield return new WaitForSeconds(2.0f);

        SpawnEffect spawn = Engine.mInstance.mPlayer.GetComponent<SpawnEffect>();
        spawn.Disappear();

        yield return new WaitForSeconds(0.5f);
        Engine.mInstance.mSceneMgr.SceneChange(mStage_2Pos);        
    }

    public IEnumerator Win()
    {
        yield return new WaitForSeconds(1.0f);

        Engine.mInstance.mPlayer.GetComponent<SpawnEffect>().Disappear();

        yield return new WaitForSeconds(2.0f);

        Engine.mInstance.mSceneMgr.PlayerWin();

    }

}
