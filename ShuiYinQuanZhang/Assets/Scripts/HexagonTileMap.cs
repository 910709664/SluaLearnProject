using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;
public class HexagonTileMap : MonoBehaviour
{
    Tilemap tilemap;
    
    [SerializeField]
    bool isShow;
    public Action<EventArg> OnShowRange;
    public Action<EventArg> OnCloseRange;
    private void Awake()
    {
        OnShowRange += ShowStepRange;
        OnCloseRange += CloseStepRange;
    }
    private void Start()
    {
        tilemap = transform.GetComponent<Tilemap>();

    }
    private void OnEnable()
    {
        EventManager.StartListening("ShowStepRange", OnShowRange);
        EventManager.StartListening("CloseStepRange", OnCloseRange);
    }
    private void Update()
    {
       
    }
    private void OnDisable()
    {
        EventManager.StopListening("ShowStepRange", OnShowRange);
        EventManager.StopListening("CloseStepRange", OnCloseRange);
    }
    /// <summary>
    /// 给tile添加颜色标识
    /// </summary>
    /// <param name="colour"></param>
    /// <param name="position"></param>
    /// <param name="tilemap"></param>
    private void SetTileColour(Color colour, Vector3Int position, Tilemap tilemap)
    {
        // Flag the tile, inidicating that it can change colour.
        // By default it's set to "Lock Colour".
        tilemap.SetTileFlags(position, TileFlags.None);

        // Set the colour.
        tilemap.SetColor(position, colour);
    }
    /// <summary>
    /// 显示移动范围and攻击范围
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="stepStepRange"></param>
    public void ShowStepRange(EventArg eventArg)
    {
        List<Vector3Int> MoveRange = GameManager.Instance.GetMoveRange(eventArg.vector, eventArg.StepRange);
        List<Vector3Int> attackRange = GameManager.Instance.GetAttackRange(MoveRange,eventArg.vector, eventArg.StepRange + eventArg.AttackRange);
        if (!isShow)
        {
            foreach (var v in MoveRange)
            {
                if (tilemap.HasTile(v))
                {
                    SetTileColour(Color.blue, v, tilemap);
                }
            }
            foreach(var v in attackRange)
            {
                if (tilemap.HasTile(v))
                {
                    SetTileColour(Color.red, v, tilemap);
                }
            }
            isShow = true;
        }
            
    }
    public void CloseStepRange(EventArg eventArg)
    {
        List<Vector3Int> MoveRange = GameManager.Instance.GetTileVector3Int(eventArg.vector, eventArg.StepRange, out MoveRange);
        List<Vector3Int> AttackRange = GameManager.Instance.GetTileVector3Int(eventArg.vector, eventArg.StepRange + eventArg.AttackRange, out AttackRange);
        AttackRange.Except(MoveRange);
        if (isShow)
        {
            foreach (var v in MoveRange)
            {
                if (tilemap.HasTile(v))
                {
                    SetTileColour(Color.white, v, tilemap);
                }
            }
            foreach (var v in AttackRange)
            {
                if (tilemap.HasTile(v))
                {
                    SetTileColour(Color.white, v, tilemap);
                }
            }
            isShow = false;
        }
    }
}

