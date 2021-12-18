using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform origin;
    private Transform rootItemPos;
    public void OnBeginDrag(PointerEventData eventData)
    {
        origin = transform.parent;
        rootItemPos = transform;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            transform.position = origin.position;
            transform.SetParent(origin);
        }
        else
        {
            GameObject eventGO = eventData.pointerCurrentRaycast.gameObject;
            if (eventGO.tag == "Slot")
            {
                transform.SetParent(eventGO.transform);
                transform.position = eventGO.transform.position;
            }
            else if (eventGO.name == "Item")
            {
                Transform item = eventGO.transform;
                transform.SetParent(item.parent);
                transform.position = item.parent.position;

                item.parent.position = origin.position;
                item.SetParent(origin);
            }
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
