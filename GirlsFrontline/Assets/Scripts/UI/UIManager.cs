using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager mInstance;
    public static UIManager Instance
    {
        get { return mInstance; }
    }
    private Dictionary<string, GameObject> mPanelDic;
    public List<GameObject> mSlotList;
    public int TeamNum;//梯队编号
    public int RoleNum;
    public Inventory TeamInventory;//队伍库
    public Inventory AllInventory;//仓库
    public Inventory FormatInventory;//阵型库
    public GameObject RoleImage;
    public GameObject Role;
    public Transform LauchPos;//出击位置
    public GameObject TeamButtons;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        mInstance = this;
        DontDestroyOnLoad(this);

        mPanelDic = new Dictionary<string, GameObject>();
        mSlotList = new List<GameObject>();

        for(int i = 0; i < transform.childCount; i++)
        {
            mPanelDic.Add(transform.GetChild(i).name, transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < 5; i++)
        {
            mSlotList.Add(RoleImage.transform.GetChild(i).gameObject);
        }
    }
    private void CloseAllPanel()
    {
        foreach(var panel in mPanelDic)
        {
            panel.Value.SetActive(false);
        }
    }
    public void ShowPanel(string panelName)
    {
        CloseAllPanel();
        if (mPanelDic[panelName] != null && mPanelDic[panelName].activeSelf == false)
        {
            mPanelDic[panelName].SetActive(true);
        } 
    }
    public void HidePanel(string panelName)
    {
        if (mPanelDic[panelName] != null && mPanelDic[panelName].activeSelf == true)
        {
            mPanelDic[panelName].SetActive(false);
        }
    }
    /// <summary>
    /// 返回
    /// </summary>
    /// <param name="panelName"></param>
    public void BackButton(string panelName)
    {
        mPanelDic[panelName].gameObject.SetActive(false);
    }
    /// <summary>
    /// 出击
    /// </summary>
    /// <param name="panelName"></param>
    public void AttackButton(string panelName)
    {
        mPanelDic[panelName].gameObject.SetActive(false);
        if (TeamInventory.RoleList[TeamNum*5] != null)
        {
            Instantiate(TeamInventory.RoleList[TeamNum * 5].RoleGo, LauchPos.position, Quaternion.identity);
            TeamButtons.transform.GetChild(TeamNum).gameObject.SetActive(false);
            //TeamNum++;
        }
        else
        {
            return;
        }
    }
    /// <summary>
    /// 改变梯队
    /// </summary>
    /// <param name="n"></param>
    public void ChangeTeamButton(int n)
    {
        TeamNum = n;
        int temp = 0;
        for(int i = n*5; i < n + 5; i++)
        {
            if (TeamInventory.RoleList[i] == null)
            {
                Slot tempSlot = mSlotList[temp++].GetComponent<Slot>();
                tempSlot.SlotImage.sprite = null;
            }
            else
            {
                temp++;
                Slot tempSlot = mSlotList[i].GetComponent<Slot>();
                tempSlot.SlotRole = TeamInventory.RoleList[i];
                tempSlot.SlotImage.sprite = TeamInventory.RoleList[i].RoleImage;
                tempSlot.SlotName = TeamInventory.RoleList[i].Name;
            }
        }
    }
    public void ChangeRoleButton()
    {


    }
    /// <summary>
    /// TODO:保存队形
    /// </summary>
    public void FormatButton()
    {

    }

   
}
