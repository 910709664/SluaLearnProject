using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OrganizePanel : MonoBehaviour
{
    public GameObject RolePanel;
    public GameObject Role;
    private List<GameObject> SlotList;
    private void Awake()
    {
        Init();
        SlotList = new List<GameObject>();
    }
    private void Start()
    {
        
        if (UIManager.Instance.AllInventory.RoleList.Count > 0)
        {
            for (int i = 0; i < UIManager.Instance.AllInventory.RoleList.Count; i++)
            {
                SlotList.Add(Instantiate(Role, RolePanel.transform));
                if (UIManager.Instance.AllInventory.RoleList[i] != null)
                {
                    
                    Slot tempSlot = SlotList[i].GetComponent<Slot>();
                    tempSlot.SlotId = i;
                    tempSlot.SlotRole = UIManager.Instance.AllInventory.RoleList[i];
                    tempSlot.SlotImage.sprite = UIManager.Instance.AllInventory.RoleList[i].RoleImage;
                    
                }
                
            }
        }


    }
    private void Init()
    {
        Role.GetComponent<Image>().sprite = null;
        Role.transform.GetChild(0).GetComponent<Image>().sprite = null;
    }
}
