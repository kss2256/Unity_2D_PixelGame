using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.EventSystems;

public class TitleMenu : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform mMarkTr;

    
    private void Awake()
    {
        mMarkTr = gameObject.transform.Find("Mark");
            
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        Engine.mInstance.mAudioMgr.PlaySfx(AudioMgr.SfxType.DRAG);
        mMarkTr.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData _value)
    {
  
    }

    public void OnPointerExit(PointerEventData _value)
    {
        mMarkTr.gameObject.SetActive(false);
    }




}
