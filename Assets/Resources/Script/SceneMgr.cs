using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public enum SceneType
    {
        TITLE,
        STAGE_1,
        STAGE_2,
        STAGE_3,
        END,
    }


    private SceneType mCurrentScene;
    [SerializeField]
    private Image mFadeImgae;

    private Color mFadeColor;

    private float mFadeTime;
    private float mFadeMaxTime = 1.0f;
    private float mFadeDuration = 3.0f;

    

    public SceneType CurScene
    {
        get { return mCurrentScene; }
        set { mCurrentScene = value; }
    }




    private void Awake()
    {
        CurScene = SceneType.TITLE;
        DontDestroyOnLoad(this);
        mFadeColor = mFadeImgae.color;
    }

    


    public void NextScene()
    {
        if (mCurrentScene == SceneType.END)
        {
            Debug.Log("SceneType == END");
            return;
        }
        mCurrentScene++;
    }
    public void SceneChange()
    {
        StartCoroutine(FadeInOut());
        StartCoroutine(ReCall(new Vector3(7.0f, -4.49f, 0.0f)));
    }

    public IEnumerator FadeInOut()
    {
        while (mFadeTime <= mFadeMaxTime)
        {
            Debug.Log("FadeIn");
            mFadeTime += Time.deltaTime / mFadeDuration;
            
            mFadeColor.a = Mathf.Lerp(0, 1, mFadeTime);
            mFadeImgae.color = mFadeColor;
            yield return null;
        }

        mFadeTime = 1.0f;

        while (mFadeTime >= 0.0f)
        {
            Debug.Log("FadeOut");
            mFadeTime -= Time.deltaTime / mFadeDuration;            
            mFadeColor.a = Mathf.Lerp(0, 1, mFadeTime);
            mFadeImgae.color = mFadeColor;
            yield return null;
        }
    }



   
    private IEnumerator ReCall(Vector3 _pos)
    { 

        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene("PlayScene");
        NextScene();
        Engine.mInstance.mPlayer.gameObject.SetActive(false);

        Engine.mInstance.mPlayer.GetComponent<Transform>().position = _pos;

        yield return new WaitForSeconds(2.0f);
        Transform tr = GameObject.Find("Stage_1").GetComponent<Transform>();
        Transform tr1 = GameObject.Find("STAGE").GetComponent<Transform>();
        Transform tr2 = tr1.transform.Find("Stage_2").GetComponent<Transform>();
        Engine.mInstance.mPlayer.gameObject.SetActive(true);
        Engine.mInstance.mPlayer.GetComponent<SpawnEffect>().Appear();        
    }

}
