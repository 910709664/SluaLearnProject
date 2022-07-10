using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SelectPanel : MonoBehaviour
{
    private void Start()
    {

        UIManager.Instance.ChangeTeamButton(UIManager.Instance.TeamNum);
    }
    private void OnEnable()
    {
        UIManager.Instance.ChangeTeamButton(UIManager.Instance.TeamNum);
    }
}
