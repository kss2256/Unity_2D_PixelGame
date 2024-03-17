using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingMenu : MonoBehaviour
    ,IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    private Vector3 mMousePos;

    private bool mbIsDrag = false;
    private bool mbMenu = false;
    private RectTransform mCanvasTr;
    private Vector2 mOffset;

 

    private void Awake()
    {
        
        mCanvasTr = GetComponent<RectTransform>();
        

    }

    private void Update()
    {
       
        if (mbIsDrag)
        {
            // MousePos-> CanvasPos
            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                mCanvasTr.parent as RectTransform,
                Input.mousePosition,
                null,
                out mousePos
            );
                
            // UIPos-> MouseMovePos
            mCanvasTr.localPosition = mousePos + mOffset;
        }




    }


    public void OnPointerDown(PointerEventData _value)
    {
        
        mbIsDrag = true;

        // Ui - Mouse = MouseMovePos;
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mCanvasTr.parent as RectTransform,
            Input.mousePosition,
            null,
            out localPos
        );
        mOffset = new Vector2(mCanvasTr.localPosition.x - localPos.x, mCanvasTr.localPosition.y - localPos.y);

        

        Debug.Log(mMousePos);
    }
    public void OnDrag(PointerEventData _value)
    {
       
        Debug.Log("Mouse Drag");
    }
    public void OnPointerUp(PointerEventData _value)
    {
        mbIsDrag = false;
        Debug.Log("Mouse Up");
    }

    public void MenuOnOff()
    {
        if(mbMenu)
        {
            OffMenu();
        }
        else
        {
            OnMenu();
        }

    }
    public void OnMenu()
    {
        gameObject.SetActive(true);
        mbMenu = true;
    }
    public void OffMenu()
    {
        gameObject.SetActive(false);
        mbMenu = false;
    }
 


}
