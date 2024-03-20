using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterGroup : MonoBehaviour
{  

    private GameObject mCurObj;
    private GameObject mPrevObj;

    private Material    mMaterial;
    private Shader      mShader;
    private Image mIconImage;
    private Color mPrevColor;

    private float mMaxValue = 1.0f;
    private float mMinValue = 0.0f;
    private float mCurTime = 1.0f;
    private float mMultiply = -1.0f;



    private bool mbIsScelect = false;


    private void Awake()
    {
        mShader = (Shader)Resources.Load("Material\\BurnShader");

        Transform[] tr = GetComponentsInChildren<Transform>();

        for (int i = 0; i < tr.Length; i++)
        {
            if(tr[i].tag == "CharacterGroup")
            {
                Material newMaterial = new Material(mShader);
                tr[i].gameObject.GetComponent<Image>().material = newMaterial;
            }
            

        }

    }

    private void Update()
    {
        if(mbIsScelect)
        {
            mCurTime += Time.deltaTime *  mMultiply;
            if (mCurTime <= mMinValue)
            {
                mCurTime = mMinValue;
                mbIsScelect = false;
                mIconImage.color = Color.gray;
            }
            mMaterial.SetFloat("_Fade", mCurTime);
        }

    }


    

    public void ScelectGameObject(GameObject _obj)
    {
        mbIsScelect = true;
        if(mMaterial)
        {
            mMaterial.SetFloat("_Fade", mMaxValue);
            mIconImage.color = mPrevColor;
        }
        
        mMaterial = _obj.gameObject.GetComponent<Image>().material;        
        mCurTime = mMaxValue;


        Transform[] tr = _obj.GetComponentsInChildren<Transform>();

        for (int i = 0; i < tr.Length; i++)
        {
            if (tr[i].name == "Icon")
            {
                mIconImage = tr[i].GetComponent<Image>();
                mPrevColor = tr[i].GetComponent<Image>().color;
                break;
            }
        }



    }

}
