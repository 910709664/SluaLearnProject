using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager mInstance;
    public static UIManager Instance
    {
        get
        {
            return mInstance;
        }
    }
    public Text RoundText;
    public GameObject RoleMsg;
    Dictionary<string, GameObject> mDic = new Dictionary<string, GameObject>();
    private void Awake()
    {
        mInstance = this;
        ReadDic();
    }
    private void Start()
    {
        //RoundText = GameObject.Find("RoundNum").GetComponent<Text>();
        RoundText.text = GameManager.Instance.RoundNum.ToString();
        //RoleMsg = GameObject.Find("Role");
        
    }
    private void ReadDic()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if (!mDic.ContainsKey(transform.GetChild(i).name))
            {
                mDic.Add(transform.GetChild(i).name, transform.GetChild(i).gameObject);
            }
        }
    }
    public void EndRound()
    {
        int num=++GameManager.Instance.RoundNum;
        RoundText.text = num.ToString();
        GameManager.Instance.RoundContinute();
        CloseRoleMsg();
    }
    public void ShowRoleMeg(Transform Role)
    {
        if (!RoleMsg.activeSelf)
        {
            RoleMsg.SetActive(true);
        }
        Image roleImage = RoleMsg.transform.GetChild(0).GetComponent<Image>();
        Text roleHP = RoleMsg.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        Text roleAtk = RoleMsg.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        roleImage.sprite=Role.GetComponent<Util>().RoleImage;
        roleHP.text = Role.GetComponent<Util>().HP.ToString();
        roleAtk.text = Role.GetComponent<Util>().Atk.ToString();

    }
    public void CloseRoleMsg()
    {
        if (RoleMsg.activeSelf)
            RoleMsg.SetActive(false);
    }
    public void ShowPanel(string name)
    {
        mDic[name].SetActive(true);
    }
    public void ClosePanel(string name)
    {
        mDic[name].SetActive(false);
    }
}
