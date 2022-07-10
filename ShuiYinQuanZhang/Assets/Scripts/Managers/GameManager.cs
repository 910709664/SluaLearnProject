using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager mInstance;
    public static GameManager Instance
    {
        get
        {
            return mInstance;
        }
    }
    HexagonAstar Astar;
    Tilemap Terrain;
    Transform mPlayer;
    public int RoundNum = 1;
    private void Awake()
    {
        mInstance = this;
    }
    private void Start()
    {
        Astar = new HexagonAstar();
        Terrain = GameObject.Find("Terrain").GetComponent<Tilemap>();
    }

    /// <summary>
    /// 获取玩家所能走的范围的瓦片
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="stepStepRange"></param>
    /// <param name="vector3s"></param>
    /// <returns></returns>
    public List<Vector3Int> GetTileVector3Int(Vector3Int vector, int stepStepRange, out List<Vector3Int> vector3s)
    {
        vector3s = new List<Vector3Int>();
        vector3s = Instance.GetRangeTiles(vector, stepStepRange);
        return vector3s;
    }
    /// <summary>
    /// 获得六边形周围的坐标
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public List<Vector3Int> GetAroundTile(Vector3Int vector)//-3 3
    {
        int index = 0;
        List<Vector3Int> result = new List<Vector3Int>();
        Vector3Int[] vector3s = new Vector3Int[6];
        //奇偶列坐标不同
        if (Mathf.Abs( vector.y % 2) > 0)//坐标存在负数,取绝对值
        {   
            vector3s[index++] = new Vector3Int(vector.x + 1, vector.y, 0);
            vector3s[index++] = new Vector3Int(vector.x + 1, vector.y + 1, 0);
            vector3s[index++] = new Vector3Int(vector.x, vector.y + 1, 0);
            vector3s[index++] = new Vector3Int(vector.x - 1, vector.y, 0);
            vector3s[index++] = new Vector3Int(vector.x, vector.y - 1, 0);
            vector3s[index] = new Vector3Int(vector.x + 1, vector.y - 1, 0);
            foreach(var temp in vector3s)
            {
                if (Terrain.HasTile(temp))//判断是否可以行走
                {
                    result.Add(temp);
                }
            }

        }
        else
        {
            vector3s[index++] = new Vector3Int(vector.x + 1, vector.y, 0);
            vector3s[index++] = new Vector3Int(vector.x - 1, vector.y + 1, 0);
            vector3s[index++] = new Vector3Int(vector.x, vector.y + 1, 0);
            vector3s[index++] = new Vector3Int(vector.x - 1, vector.y, 0);
            vector3s[index++] = new Vector3Int(vector.x, vector.y - 1, 0);
            vector3s[index] = new Vector3Int(vector.x - 1, vector.y - 1, 0);
            foreach (var temp in vector3s)
            {
                if (Terrain.HasTile(temp))
                {
                    result.Add(temp);
                }
            }
        }
        return result;
    }
    /// <summary>
    /// 递归获得步数范围内的坐标
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public List<Vector3Int> GetRangeTiles(Vector3Int vector,int range)
    {
        if (range== 0) return null;
        if (range == 1)
        {
            return GetAroundTile(vector);
        }
        List<Vector3Int> result = new List<Vector3Int>();
        List<Vector3Int> aroundTile = new List<Vector3Int>(6);
        List<Vector3Int> tiles = Instance.GetRangeTiles(vector, range - 1);//递归获得步数-1的范围坐标
        HashSet<Vector3Int> hs = new HashSet<Vector3Int>();
        foreach(var tile in tiles)
        {
            hs.Add(tile);
            aroundTile =Instance.GetAroundTile(tile);
            foreach (var t in aroundTile)
            {
                if (t != vector)
                {
                    if (!hs.Contains(t)&&Terrain.HasTile(t))
                    {
                        hs.Add(t);
                    }
                }
            }
        }
        foreach (var v in hs)
        {
            result.Add(v);
        }
        return result;
    }
    /// <summary>
    /// 判断tile上是否有障碍物
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="tilemap"></param>
    /// <returns></returns>
    public bool IsPassCell(Vector3Int vector,Tilemap tilemap)
    {
        Vector3 pos = tilemap.CellToWorld(vector);
        Collider2D collider = Physics2D.OverlapCircle(pos, 0.3f, 1 << LayerMask.NameToLayer("Enemy"));//TODO 障碍物
        if (collider != null) return false;
        return true;
    }
    /// <summary>
    /// 移动范围
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    public List<Vector3Int> GetMoveRange(Vector3Int vector,int StepRange)
    {
        List<Vector3Int> range = Instance.GetRangeTiles(vector, StepRange);
        List<Vector3Int> result = new List<Vector3Int>();
        foreach(var v in range)
        {
            if (IsPassCell(v, Terrain))
            {
                result.Add(v);
            }
        }
        //result.AddRange(range);
        return result;
    }
    /// <summary>
    /// 攻击范围
    /// </summary>
    /// <param name="moveRange"></param>
    /// <param name="vector"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public List<Vector3Int> GetAttackRange(List<Vector3Int> moveRange,Vector3Int vector,int range)
    {
        List<Vector3Int> attackRange =Instance.GetTileVector3Int(vector, range, out attackRange);
        var result=attackRange.Except(moveRange);
        return result.ToList<Vector3Int>();
    }
    /// <summary>
    /// A*寻路
    /// </summary>
    /// <param name="start"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public List<Hexagon> AstarSearch(Hexagon start, Hexagon target)
    {
        List<Hexagon> route=Astar.AstarSearch(start, target,Terrain);
        return route;
    }
    /// <summary>
    /// 玩家移动
    /// </summary>
    /// <param name="player"></param>
    /// <param name="route"></param>
    public void PlayerMoveToTarget(Transform player,List<Hexagon> route,RaycastHit2D hit)
    {
        if (route == null) return;
        mPlayer = player;
        List<Vector3> path = new List<Vector3>();
        float y = player.GetComponent<SpriteRenderer>().bounds.center.y;//计算sprite与坐标点的y的差值
        //转换成世界坐标
        for(int i = 0; i < route.Count; i++)
        {
            path.Add(hit.transform.GetComponent<Tilemap>().CellToWorld(route[i].vector));
        }
        if (path != null)
        {
            y = Mathf.Abs(y - path[0].y);
        }
        for(int i = 1; i < path.Count; i++)
        {
            path[i] = new Vector3(path[i].x, path[i].y + y, 0);//转换成人物移动的实际世界坐标
        }
        StartCoroutine(Move(path,player));

    }
    IEnumerator Move(List<Vector3> route, Transform player)
    {
        for (int i = 1; i < route.Count; i++)
        {
            player.DOMove(route[i],0.3f);
            yield return new WaitForSeconds(0.3f);
        }
        MouseManager.Instance.isClick = true;
        //player.GetComponent<Player>().IsAction = true;
    }
    /// <summary>
    /// 每回合开始时
    /// </summary>
    public void RoundContinute()
    {   
        mPlayer.GetComponent<Player>().IsAction = false;    
    }

}

