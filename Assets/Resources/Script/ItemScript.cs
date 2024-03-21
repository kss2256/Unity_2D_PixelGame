using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Engine.mInstance.mAudioMgr.PlaySfx(AudioMgr.SfxType.ITEM);

            gameObject.SetActive(false);
        }




    }




}
