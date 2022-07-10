using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class FormationPanel : MonoBehaviour
{
    public GameObject Format;
    public Transform Formations;
    private List<GameObject> Slots;
    
    private void Awake()
    {
        Slots = new List<GameObject>();
    }

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        int tempTeamNum = UIManager.Instance.TeamNum*5;
        for (int i = 0; i < 9; i++)
        {
            Slots.Add(Instantiate(Format, Formations));
        }
        //TODO:数据读取
        for(int j = 0; j < 5; j++)
        {
            if (UIManager.Instance.TeamInventory.RoleList[tempTeamNum] != null)
            {
                Slots[j].transform.GetChild(0).gameObject.AddComponent<RoleOnDrag>();//给存在梯队的角色添加拖拽脚本
                Slots[j].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = UIManager.Instance.TeamInventory.RoleList[tempTeamNum].ModImage;
                Slots[j].transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                UIManager.Instance.FormatInventory.FormatList[UIManager.Instance.TeamNum].FormatNum[j] = UIManager.Instance.TeamInventory.RoleList[tempTeamNum++];//将阵型中的角色信息存储
            }
            else
            {
                break;
            }
        }

    }


}
