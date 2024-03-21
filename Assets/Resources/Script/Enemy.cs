using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private WaitForFixedUpdate mWaitFixedUpdate;

    private void Awake()
    {
        mWaitFixedUpdate = new WaitForFixedUpdate();       
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

        Engine.mInstance.mPlayer.GetComponent<HpScript>().PopHp();
        Engine.mInstance.mPlayer.Hit();
        Engine.mInstance.mAudioMgr.PlaySfx(AudioMgr.SfxType.HIT);

        Vector3 playerPos = _target.transform.position;
        Vector3 dirVec = playerPos - transform.position;
        Rigidbody2D mRigidbody = _target.GetComponent<Rigidbody2D>();
        mRigidbody.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);


        //FixedUpdate 1프레임 멈추는
        yield return mWaitFixedUpdate;

        if(Engine.mInstance.mPlayer.Hp <= 0)
        {
            Engine.mInstance.mPlayer.mbIsDead = true;
            Engine.mInstance.mPlayer.GetComponent<SpawnEffect>().Disappear();
            yield return new WaitForSeconds(3f);
            Engine.mInstance.mPlayer.gameObject.SetActive(false);
            Engine.mInstance.mSceneMgr.PlayerDead();
        }

       
    }

}
