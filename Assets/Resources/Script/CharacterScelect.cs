using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterScelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private delegate float FunctionAddTime();

    private bool    mbCurSclect = false;
    private Vector3  mImgMinSize = Vector3.one;
    private Vector3  mImgMaxSize = Vector3.one * 1.3f;
 

    private RectTransform mRectTr;
    

    private FunctionAddTime mFuncAdd;


    private void Awake()
    {
        mRectTr = GetComponent<RectTransform>();
    }

    private void Update()
    {

        
        if (mbCurSclect)
        {
            if (mRectTr.localScale.x >= mImgMaxSize.x)
            {
                mRectTr.localScale = mImgMaxSize;                
            }
            else
            mRectTr.localScale += mRectTr.localScale * Time.deltaTime * mFuncAdd(); 
        }
        else
        {
            if (mRectTr.localScale.x <= mImgMinSize.x)
            {
                mRectTr.localScale = mImgMinSize;
            }
            else
            mRectTr.localScale += mRectTr.localScale * Time.deltaTime * mFuncAdd();
        }



    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        mbCurSclect = true;
        mFuncAdd = Up;
        Debug.Log(gameObject.name + "Mouse In Scelect Box");
    }
    
  
    
    public void OnPointerExit(PointerEventData eventData)
    {
        mbCurSclect = false;
        mFuncAdd = Down;
        Debug.Log(gameObject.name + "Mouse Out Scelect Box");
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
