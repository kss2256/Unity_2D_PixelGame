using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.UI;

public class HpScript : MonoBehaviour
{

    [SerializeField]
    private GameObject mHpBar;
    [SerializeField]
    private GameObject mHpPrefab;

    private Stack<GameObject> mHpObjs = new Stack<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

        CreateHp();
       

   


        //mHpBar.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PushHp();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PopHp();
        }

    }



    private void CreateHp()
    {
        int hp = Engine.mInstance.mPlayer.Hp;

        for (int i = 0; i < hp; i++)
        {
            mHpObjs.Push(Instantiate<GameObject>(mHpPrefab, mHpBar.transform));
        }
    }

    public void PopHp()
    {
        if (mHpObjs.Count > 0)
        {            
            GameObject dlelteObj = mHpObjs.Pop();
            Destroy(dlelteObj);
            --Engine.mInstance.mPlayer.Hp;
        }
    }

    public void PushHp()
    {
        int hp = Engine.mInstance.mPlayer.Hp;
        if (hp < Engine.mInstance.mPlayer.MaxHp)
        {            
            mHpObjs.Push(Instantiate<GameObject>(mHpPrefab, mHpBar.transform));
            ++Engine.mInstance.mPlayer.Hp;
        }
          
    }

}
