using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{

    public enum BgmType
    {
        STAGE_1,
        STAGE_2,
        TITLEBGM,
    }
    public enum SfxType
    {
        CLICK,
        DRAG,
        END_POINT,
        FIRE,
        HIT,
        JUMP,
        APPEAR,
        APPEARING,
        DEAD,
        DISAPPER,
        WALK,
    }

    [Header("#BGM")]
    public AudioClip[] mBgmClips;
    public float mBgmVolume;
    public int mBgmChannel;
    private AudioSource mBgmPlayers;

    [Header("#SFX")]
    public AudioClip[] mSfxClips;
    public float mSfxVolume;
    public int mSfxChannel;
    private AudioSource[] mSfxPlayers;
    private int mCSfxhannelIndex;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        Init();
    }


    private void Init()
    {
        //Bgm Init
        GameObject bgmObj = new GameObject("BgmPlayer");
        bgmObj.transform.parent = transform;
        mBgmPlayers = bgmObj.AddComponent<AudioSource>();
        mBgmPlayers.playOnAwake = false;
        mBgmPlayers.loop = true;
        mBgmPlayers.volume = mBgmVolume;

        //Sfx Init
        GameObject sfxObj = new GameObject("SfxPlayer");
        sfxObj.transform.parent = transform;
        mSfxPlayers = new AudioSource[mSfxChannel];
        for (int i = 0; i < mSfxChannel; i++)
        {
            mSfxPlayers[i] = sfxObj.AddComponent<AudioSource>();
            mSfxPlayers[i].playOnAwake = false;
            mSfxPlayers[i].volume = mSfxVolume;            
        }
    }


    public void PlayBgm(BgmType _type)
    {        
        mBgmPlayers.clip = mBgmClips[(int)_type];
        mBgmPlayers.Play();
    }


    public void PlaySfx(SfxType _type)
    {
        /*mSfxChannel 갯수만큼의 여러 사운드를 재생 시킬수있게 제작.
         채널 개수만큼 돌면서 채널 인덱스가 플레이 되고있으면 다음 으로
         넘어가서 클립을 넣어준다 enum과 클립스에 있는 걸 맞춰야함.
         */

        for (int i = 0; i < mSfxChannel; i++)
        {
            int iterator = (i + mCSfxhannelIndex) % mSfxChannel;

            if(_type == SfxType.WALK)
            {
                if(mSfxPlayers[iterator].isPlaying)
                    break;
            }

            if (mSfxPlayers[iterator].isPlaying)
                continue;

            mCSfxhannelIndex = iterator;
            mSfxPlayers[iterator].clip = mSfxClips[(int)_type];
            mSfxPlayers[iterator].Play();

        }
    }



}
