using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Engine : MonoBehaviour
{

    public static Engine mInstance;

    [Header ("# Game Part")]
    public Player mPlayer;
    public SceneMgr mSceneMgr;
    public AudioMgr mAudioMgr;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        mInstance = this;

    }


}
