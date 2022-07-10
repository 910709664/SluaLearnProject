using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class RoleOnDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform mOriginParent;
    private Transform mFormat;
    private Vector3 mOffset;
    private RectTransform mRect;
    private void Awake()
    {
        mFormat = transform.parent.parent.parent.transform;
        mRect = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
         mOriginParent = transform.parent;
         // 存储点击时的鼠标坐标
         Vector3 tWorldPos;
         //UI屏幕坐标转换为世界坐标
         RectTransformUtility.ScreenPointToWorldPointInRectangle(mRect, eventData.position, eventData.pressEventCamera, out tWorldPos);
         //计算偏移量   
         mOffset = transform.position - tWorldPos;
         SetDraggedPosition(eventData);
         transform.SetParent(mFormat);
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        transform.GetChild(0).GetComponent<Image>().raycastTarget = false;
        transform.SetParent(mFormat);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        Transform temp = eventData.pointerCurrentRaycast.gameObject.transform;
        Debug.Log(temp.name);
        if (temp.name == "RoleImage")
        {   
            //在空位上
            if (!temp.GetComponent<Image>().sprite)
            {
                Transform currentParent = temp.parent;
                temp.SetParent(mOriginParent);
                transform.SetParent(currentParent);
                transform.position = temp.position;
                temp.position = mOriginParent.position;
                //TODO:阵型数据保存
            }
            //交换
            else
            {
                //TODO
            }
        }
        else
        {
            SetOrigin();//复位
        }

      
        transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
    }


    private void SetDraggedPosition(PointerEventData eventData)
   {
        //存储当前鼠标所在位置
         Vector3 globalMousePos;
        //UI屏幕坐标转换为世界坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(mRect, eventData.position, eventData.pressEventCamera, out globalMousePos))
         {
             //设置位置及偏移量
             mRect.position = globalMousePos + mOffset;
         }
    }
    private void SetOrigin()
    {
        transform.SetParent(mOriginParent);
        transform.position = mOriginParent.position;
    }
}
