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
    [SerializeField]
    private GameObject mFadeObj;
    [SerializeField]
    private GameObject mWinObj;
    [SerializeField]
    private GameObject mDeadObj;

    private Transform mStage_2Tr;

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
        mCurrentScene = SceneType.TITLE;
        DontDestroyOnLoad(this);
        mFadeColor = mFadeImgae.color;
        
    }
    private void Start()
    {
        Engine.mInstance.mAudioMgr.PlayBgm(AudioMgr.BgmType.TITLEBGM);
    }

    public void PlayerWin()
    {
        mWinObj.SetActive(true);
    }

    public void PlayerDead()
    {
        mDeadObj.SetActive(true);
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

    public void SceneChange(Vector3 _pos)
    {
        StartCoroutine(FadeInOut());
        StartCoroutine(ReCall(_pos));
    }
    public void SceneChange()
    {
        Engine.mInstance.mAudioMgr.PlaySfx(AudioMgr.SfxType.CLICK);
        StartCoroutine(FadeInOut());
        StartCoroutine(ReCall(new Vector3(7.0f, -4.49f, 0.0f)));
    }

    public IEnumerator FadeInOut()
    {
        mFadeObj.SetActive(true);
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
        mFadeObj.SetActive(false);
    }



   
    private IEnumerator ReCall(Vector3 _pos)
    { 

        yield return new WaitForSeconds(3.0f);

        if (mCurrentScene == SceneType.TITLE)
        {
            SceneManager.LoadScene("PlayScene");
            Engine.mInstance.mAudioMgr.PlayBgm(AudioMgr.BgmType.STAGE_1);
        }

        NextScene();
        Engine.mInstance.mPlayer.GetComponent<Transform>().position = _pos;
        yield return null;
        Engine.mInstance.mPlayer.gameObject.SetActive(false);
                
        switch (mCurrentScene)
        {
            case SceneType.STAGE_1:
                {                    
                    Transform tr1 = GameObject.Find("STAGE").GetComponent<Transform>();
                    mStage_2Tr = tr1.transform.Find("Stage_2").GetComponent<Transform>();
                }
                break;
            case SceneType.STAGE_2:
                {
                    Engine.mInstance.mAudioMgr.PlayBgm(AudioMgr.BgmType.STAGE_2);
                    Transform tr1 = GameObject.Find("Stage_1").GetComponent<Transform>();
                    tr1.gameObject.SetActive(false);
                    mStage_2Tr.gameObject.SetActive(true);
                }
                break;
            case SceneType.STAGE_3:
                break;
            case SceneType.END:
                break;
            default:
                break;
        }
        
        Engine.mInstance.mPlayer.gameObject.SetActive(true);
        Engine.mInstance.mPlayer.GetComponent<SpawnEffect>().Appear();        
    }

}
