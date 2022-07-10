using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TeamRolePoint : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Instance.RoleNum = eventData.pointerEnter.transform.parent.GetComponent<Slot>().SlotId;
        UIManager.Instance.ShowPanel("StorePanel");
    }
}
