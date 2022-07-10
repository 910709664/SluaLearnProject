using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DepositoryRolePoint :MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Transform Role = eventData.pointerEnter.transform.parent;//仓库位置
        Slot RoleGo = UIManager.Instance.mSlotList[UIManager.Instance.RoleNum].GetComponent<Slot>();//梯队位置
        RoleGo.SlotRole = Role.GetComponent<Slot>().SlotRole;
        RoleGo.SlotImage.sprite = Role.GetComponent<Slot>().SlotImage.sprite;
        UIManager.Instance.ShowPanel("SelectPanel");
        
    }
}
