using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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

    



    public SceneType CurScene
    {
        get { return mCurrentScene; }
        set { mCurrentScene = value; }
    }




    private void Awake()
    {
        CurScene = SceneType.TITLE;
        DontDestroyOnLoad(this);
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
        SceneManager.LoadScene("PlayScene");
        //mCurrentScene = SceneType.TITLE;
        NextScene();
        Engine.mInstance.mPlayer.gameObject.SetActive(false);
        StartCoroutine(ReCall(new Vector3(7.0f, -4.49f, 0.0f)));
    }

   
    private IEnumerator ReCall(Vector3 _pos)
    {
        Engine.mInstance.mPlayer.GetComponent<Transform>().position = _pos;

        yield return new WaitForSeconds(2.0f);
        Engine.mInstance.mPlayer.gameObject.SetActive(true);
        Engine.mInstance.mPlayer.GetComponent<SpawnEffect>().Appear();
        
    }

}
